using Application.DTOs.Quizzes;
using Application.Interfaces.Service;
using Application.Interfaces.UnitOfWork;
using Application.Interfaces.UserService;
using Application.Results;
using Domain.Entities;
using Domain.Enums;

namespace Application.Services
{
    public class QuizService : IQuizService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;

        public QuizService(IUnitOfWork unitOfWork,IUserService userService)
        {
            _unitOfWork = unitOfWork;
            _userService = userService;
        }

        public async Task<Result<Guid>> CreateAsync(QuizRequest request)
        {
            var questions = await _unitOfWork.Questions.GetAllAsync(
                x => request.QuestionIds.Contains(x.Id)
                     && x.TenantId == _userService.TenantId
                     && !x.IsDeleted
                     && !x.IsLocked);

            if (questions.Count != request.QuestionIds.Count)
            {
                return Result<Guid>.Failure(
                    Errors.QuizError.InvalidQuestions);
            }

            var quiz = new Quiz
            {
                Id = Guid.NewGuid(),
                TenantId = _userService.TenantId,
                CreatedByUserId = _userService.UserId,

                Title = request.Title,
                Description = request.Description,
                MaxAttempts = request.MaxAttempts,

                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            quiz.QuizQuestions = request.QuestionIds
                .Select((questionId, index) => new QuizQuestion
                {
                    QuizId = quiz.Id,
                    QuestionId = questionId,
                    DisplayOrder = index + 1
                })
                .ToList();

            await _unitOfWork.Quizzes.AddAsync(quiz);
            await _unitOfWork.SaveChangesAsync();

            return Result<Guid>.Success(quiz.Id);
        }

        public async Task<Result<IReadOnlyList<AvailableQuizResponseDto>>> GetAvailableForStudentAsync()
        {
            var studentId = _userService.UserId;
            var tenantId = _userService.TenantId;

            var quizzes = await _unitOfWork.Quizzes.GetAllAsync(
                x => x.TenantId == tenantId && x.IsActive,
                x => x.QuizQuestions,
                x => x.Attempts.Where(a => a.StudentId == studentId));

            var availableQuizzes = quizzes
                .Where(quiz =>
                {
                    var attemptsUsed = quiz.Attempts.Count;

                    // Unlimited attempts
                    if (!quiz.MaxAttempts.HasValue)
                        return true;

                    // Still has attempts
                    return attemptsUsed < quiz.MaxAttempts.Value;
                })
                .Select(quiz =>
                {
                    var attemptsUsed = quiz.Attempts.Count;

                    return new AvailableQuizResponseDto
                    {
                        Id = quiz.Id,
                        Title = quiz.Title,
                        Description = quiz.Description,

                        MaxAttempts = quiz.MaxAttempts,

                        AttemptsUsed = attemptsUsed,

                        AttemptsRemaining = quiz.MaxAttempts.HasValue
                            ? quiz.MaxAttempts.Value - attemptsUsed
                            : null,

                        TotalQuestions = quiz.QuizQuestions.Count
                    };
                })
                .ToList();

            return Result<IReadOnlyList<AvailableQuizResponseDto>>
                .Success(availableQuizzes);
        }

        public async Task<Result<StartQuizResponseDto>> StartQuizAsync(Guid quizId)
        {
            var studentId = _userService.UserId;
            var tenantId = _userService.TenantId;

            // 1. Get quiz
            var quiz = await _unitOfWork.Quizzes.FirstOrDefaultAsync(
                x => x.Id == quizId &&
                     x.TenantId == tenantId &&
                     x.IsActive,
                x => x.QuizQuestions);

            if (quiz is null)
            {
                return Result<StartQuizResponseDto>.Failure(
                    Errors.QuizError.NotFound);
            }

            // 2. Check attempts
            var attemptsUsed = await _unitOfWork.QuizAttempts.CountAsync(
                x => x.QuizId == quizId &&
                     x.StudentId == studentId &&
                     x.TenantId == tenantId);

            if (quiz.MaxAttempts.HasValue &&
                attemptsUsed >= quiz.MaxAttempts.Value)
            {
                return Result<StartQuizResponseDto>.Failure(
                    Errors.QuizError.MaximumAttemptsExceeded);
            }

            // 3. Get questions
            var questionIds = quiz.QuizQuestions
                .OrderBy(x => x.DisplayOrder)
                .Select(x => x.QuestionId)
                .ToList();

            var questions = await _unitOfWork.Questions.GetAllAsync(
                x => questionIds.Contains(x.Id) &&
                     x.TenantId == tenantId &&
                     !x.IsDeleted &&
                     !x.IsLocked,
                x => x.Choices);

            if (!questions.Any())
            {
                return Result<StartQuizResponseDto>.Failure(
                    Errors.QuizError.NoQuestions);
            }

            // 4. Create attempt
            var attempt = new QuizAttempt
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                QuizId = quizId,
                StudentId = studentId,
                Status = AttemptStatus.InProgress,
                TotalQuestions = questions.Count,
                StartedAt = DateTime.UtcNow
            };

            await _unitOfWork.QuizAttempts.AddAsync(attempt);
            await _unitOfWork.SaveChangesAsync();

            // 5. Map questions
            var response = new StartQuizResponseDto
            {
                AttemptId = attempt.Id,
                QuizId = quiz.Id,
                Title = quiz.Title,
                TotalQuestions = questions.Count,

                Questions = questions
                    .Select(question => new QuizQuestionResponseDto
                    {
                        QuestionId = question.Id,
                        Text = question.Text,
                        ImageUrl = question.ImageUrl,

                        DisplayOrder = quiz.QuizQuestions
                            .First(x => x.QuestionId == question.Id)
                            .DisplayOrder,

                        Choices = question.Choices
                            .Select(choice => new QuizChoiceResponseDto
                            {
                                Id = choice.Id,
                                Text = choice.Text
                            })
                            .ToList()
                    })
                    .OrderBy(x => x.DisplayOrder)
                    .ToList()
            };

            return Result<StartQuizResponseDto>.Success(response);
        }

        public async Task<Result<SubmitQuizResponseDto>> SubmitQuizAsync(Guid attemptId,SubmitQuizRequest request)
        {
            var studentId = _userService.UserId;
            var tenantId = _userService.TenantId;

            // 1. Get attempt
            var attempt = await _unitOfWork.QuizAttempts.FirstOrDefaultAsync(
                x => x.Id == attemptId &&
                     x.StudentId == studentId &&
                     x.TenantId == tenantId,
                x => x.Quiz);

            if (attempt is null)
            {
                return Result<SubmitQuizResponseDto>.Failure(
                    Errors.QuizError.AttemptNotFound);
            }

            // 2. Check attempt status
            if (attempt.Status == AttemptStatus.Submitted)
            {
                return Result<SubmitQuizResponseDto>.Failure(
                    Errors.QuizError.AttemptAlreadySubmitted);
            }

            // 3. Get quiz questions
            var quizQuestions =
                await _unitOfWork.QuizQuestions.GetAllAsync(
                    x => x.QuizId == attempt.QuizId);

            if (!quizQuestions.Any())
            {
                return Result<SubmitQuizResponseDto>.Failure(
                    Errors.QuizError.QuestionsNotFound);
            }

            var questionIds = quizQuestions
                .Select(x => x.QuestionId)
                .ToList();

            // 4. Get questions + choices
            var questions = await _unitOfWork.Questions.GetAllAsync(
                x => questionIds.Contains(x.Id) &&
                     x.TenantId == tenantId &&
                     !x.IsDeleted,
                x => x.Choices);

            // 5. Validate submitted questions
            var invalidQuestion = request.Answers
                .Any(x => !questionIds.Contains(x.QuestionId));

            if (invalidQuestion)
            {
                return Result<SubmitQuizResponseDto>.Failure(
                    Errors.QuizError.InvalidQuestion);
            }

            // 6. Prevent duplicate answers
            var submittedQuestionIds = request.Answers
                .Select(x => x.QuestionId)
                .ToList();

            if (submittedQuestionIds.Count !=
                submittedQuestionIds.Distinct().Count())
            {
                return Result<SubmitQuizResponseDto>.Failure(
                    Errors.QuizError.DuplicateQuestionAnswer);
            }

            // 7. Validate submitted choices
            foreach (var answer in request.Answers)
            {
                // Null means the student skipped the question
                if (!answer.SelectedChoiceId.HasValue)
                    continue;

                var question = questions
                    .FirstOrDefault(x => x.Id == answer.QuestionId);

                if (question is null)
                {
                    return Result<SubmitQuizResponseDto>.Failure(
                        Errors.QuizError.QuestionNotFound);
                }

                var choiceExists = question.Choices
                    .Any(x => x.Id == answer.SelectedChoiceId.Value);

                if (!choiceExists)
                {
                    return Result<SubmitQuizResponseDto>.Failure(
                        Errors.QuizError.InvalidSelectedChoice);
                }
            }

            // 8. Calculate score and create QuizAnswers
            var score = 0;

            var answers = new List<QuizAnswer>();

            foreach (var question in questions)
            {
                var submittedAnswer = request.Answers
                    .FirstOrDefault(x => x.QuestionId == question.Id);

                QuestionChoice? selectedChoice = null;

                if (submittedAnswer?.SelectedChoiceId.HasValue == true)
                {
                    selectedChoice = question.Choices
                        .FirstOrDefault(x =>
                            x.Id == submittedAnswer.SelectedChoiceId.Value);
                }

                var isCorrect = selectedChoice?.IsCorrect == true;

                if (isCorrect)
                {
                    score++;
                }

                answers.Add(new QuizAnswer
                {
                    Id = Guid.NewGuid(),
                    AttemptId = attempt.Id,
                    QuestionId = question.Id,
                    SelectedChoiceId = selectedChoice?.Id,
                    IsCorrect = isCorrect,
                    CreatedAt = DateTime.UtcNow
                });
            }

            // 9. Calculate result
            var totalQuestions = questions.Count;

            var percentage = totalQuestions == 0
                ? 0
                : Math.Round(
                    (decimal)score / totalQuestions * 100,
                    2);

            // 10. Update attempt
            attempt.Status = AttemptStatus.Submitted;
            attempt.Score = score;
            attempt.TotalQuestions = totalQuestions;
            attempt.Percentage = percentage;
            attempt.SubmittedAt = DateTime.UtcNow;

            try
            {
                await _unitOfWork.BeginTransactionAsync();

                await _unitOfWork.QuizAttempts.UpdateAsync(attempt);

                // 11. Save student answers
                await _unitOfWork.QuizAnswers.AddRangeAsync(answers);

                // 12. Save changes
                await _unitOfWork.SaveChangesAsync();

                // 13. Commit transaction
                await _unitOfWork.CommitTransactionAsync();
            }
            catch
            {
                // Rollback if anything fails
                await _unitOfWork.RollbackTransactionAsync();

                throw;
            }
            // 13. Return result
            return Result<SubmitQuizResponseDto>.Success(
                new SubmitQuizResponseDto
                {
                    AttemptId = attempt.Id,
                    Score = score,
                    TotalQuestions = totalQuestions,
                    Percentage = percentage,
                    SubmittedAt = attempt.SubmittedAt.Value
                });
        }
    }
}