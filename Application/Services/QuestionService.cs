using Application.DTOs.Questions;
using Application.Interfaces.Service;
using Application.Interfaces.UnitOfWork;
using Application.Interfaces.UserService;
using Application.Results;
using Domain.Entities;

namespace Application.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;

        public QuestionService(IUnitOfWork unitOfWork,IUserService userService)
        {
            _unitOfWork = unitOfWork;
            _userService = userService;
        }


        public async Task<Result<Guid>> CreateAsync(QuestionRequest request)
        {
            var question = CreateQuestion(request);

            await _unitOfWork.Questions.AddAsync(question);
            await _unitOfWork.SaveChangesAsync();

            return Result<Guid>.Success(question.Id);
        }

        public async Task<Result<Guid>> UpdateAsync(Guid id,QuestionRequest request)
        {
            var oldQuestion = await _unitOfWork.Questions.FirstOrDefaultAsync(
                x => x.Id == id &&
                     x.TenantId == _userService.TenantId &&
                     !x.IsDeleted &&
                     !x.IsLocked);

            if (oldQuestion is null)
            {
                return Result<Guid>.Failure(Errors.QuestionError.NotFound);
            }

            // Lock old version
            oldQuestion.IsLocked = true;
            oldQuestion.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.Questions.UpdateAsync(oldQuestion);

            // Create new version
            var newQuestion = CreateQuestion(request,oldQuestion.Id);

            await _unitOfWork.Questions.AddAsync(newQuestion);

            await _unitOfWork.SaveChangesAsync();

            return Result<Guid>.Success(newQuestion.Id);
        }
        public async Task<Result<IReadOnlyList<QuestionResponseDto>>> GetAllAsync()
        {
            var questions = await _unitOfWork.Questions.GetAllAsync(
                x => x.TenantId == _userService.TenantId && !x.IsDeleted,
                x => x.Choices);

            var response = questions.Select(question => new QuestionResponseDto
            {
                Id = question.Id,
                Text = question.Text,
                ImageUrl = question.ImageUrl,
                IsLocked = question.IsLocked,
                Choices = question.Choices.Select(choice => new ChoiceResponseDto
                {
                    Id = choice.Id,
                    Text = choice.Text,
                    IsCorrect = choice.IsCorrect
                }).ToList()
            }).ToList();
            return Result<IReadOnlyList<QuestionResponseDto>>.Success(response);
        }
        public async Task<Result<QuestionResponseDto>> GetByIdAsync(Guid id)
        {
            var question = await _unitOfWork.Questions.FirstOrDefaultAsync(
                x => x.Id == id &&
                     x.TenantId == _userService.TenantId && !x.IsDeleted,
                x => x.Choices);

            if (question is null)
            {
                return Result<QuestionResponseDto>.Failure(Errors.QuestionError.NotFound);
            }

            var response = new QuestionResponseDto
            {
                Id = question.Id,
                Text = question.Text,
                ImageUrl = question.ImageUrl,
                IsLocked = question.IsLocked,
                Choices = question.Choices
                    .Select(x => new ChoiceResponseDto
                    {
                        Id = x.Id,
                        Text = x.Text,
                        IsCorrect = x.IsCorrect
                    })
                    .ToList()
            };

            return Result<QuestionResponseDto>.Success(response);
        }
        public async Task<Result<bool>> DeleteAsync(Guid id)
        {
            var question = await _unitOfWork.Questions.FirstOrDefaultAsync(
                x => x.Id == id && x.TenantId == _userService.TenantId && !x.IsDeleted && !x.IsLocked);

            if (question is null)
            {
                return Result<bool>.Failure(Errors.QuestionError.NotFound);
            }

            question.IsDeleted = true;
            question.UpdatedAt = DateTime.UtcNow;

            await _unitOfWork.Questions.UpdateAsync(question);
            await _unitOfWork.SaveChangesAsync();

            return Result<bool>.Success(true);
        }
        private Question CreateQuestion(QuestionRequest request, Guid? previousQuestionId = null)
        {
            var now = DateTime.UtcNow;
            var question = new Question
            {
                Id = Guid.NewGuid(),

                TenantId = _userService.TenantId,
                CreatedByUserId = _userService.UserId,

                Text = request.Text,
                ImageUrl = request.ImageUrl,

                IsDeleted = false,
                IsLocked = false,

                PreviousQuestionId = previousQuestionId,

                CreatedAt = now,
                UpdatedAt = null
            };

            question.Choices = request.Choices
                .Select(choiceRequest => new QuestionChoice
                {
                    Id = Guid.NewGuid(),
                    QuestionId = question.Id,

                    Text = choiceRequest.Text,
                    IsCorrect = choiceRequest.IsCorrect,

                    CreatedAt = now
                })
                .ToList();

            return question;
        }

    }
}

