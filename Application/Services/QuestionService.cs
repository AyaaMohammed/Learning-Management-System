using Application.DTOs.Questions;
using Application.Interfaces.Repositories;
using Application.Interfaces.Service;
using Application.Interfaces.UserService;
using Application.Results;
using Domain.Entities;

namespace Application.Services
{
    public class QuestionService : IQuestionService
    {
        private readonly IGenericRepositoryAsync<Question> _questionRepository;
        private readonly IGenericRepositoryAsync<QuestionChoice> _choiceRepository;
        private readonly IUserService _userService;

        public QuestionService(
            IGenericRepositoryAsync<Question> questionRepository,
            IGenericRepositoryAsync<QuestionChoice> choiceRepository,
            IUserService userService)
        {
            _questionRepository = questionRepository;
            _choiceRepository = choiceRepository;
            _userService = userService;
        }


        public async Task<Result<Guid>> CreateAsync(QuestionRequest request)
        {
            var question = CreateQuestion(request);

            await _questionRepository.AddAsync(question);
            await _questionRepository.SaveChangesAsync();

            return Result<Guid>.Success(question.Id);
        }

        public async Task<Result<Guid>> UpdateAsync(Guid id,QuestionRequest request)
        {
            var oldQuestion = await _questionRepository.FirstOrDefaultAsync(
                x => x.Id == id &&
                     x.TenantId == _userService.TenantId &&
                     !x.IsDeleted &&
                     !x.IsLocked);

            if (oldQuestion is null)
            {
                return Result<Guid>.Failure(
                    new Error("Question not found.", ErrorType.NotFound));
            }

            // Lock old version
            oldQuestion.IsLocked = true;
            oldQuestion.UpdatedAt = DateTime.UtcNow;

            await _questionRepository.UpdateAsync(oldQuestion);

            // Create new version
            var newQuestion = CreateQuestion(request,oldQuestion.Id);

            await _questionRepository.AddAsync(newQuestion);

            await _questionRepository.SaveChangesAsync();

            return Result<Guid>.Success(newQuestion.Id);
        }
        public async Task<Result<IReadOnlyList<QuestionResponseDto>>> GetAllAsync()
        {
            var questions = await _questionRepository.GetAllAsync(
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
            var question = await _questionRepository.FirstOrDefaultAsync(
                x => x.Id == id &&
                     x.TenantId == _userService.TenantId && !x.IsDeleted,
                x => x.Choices);

            if (question is null)
            {
                return Result<QuestionResponseDto>.Failure(
                    new Error("Question not found.", ErrorType.NotFound));
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
            var question = await _questionRepository.FirstOrDefaultAsync(
                x => x.Id == id && x.TenantId == _userService.TenantId && !x.IsDeleted && !x.IsLocked);

            if (question is null)
            {
                return Result<bool>.Failure(
                    new Error("Question not found.", ErrorType.NotFound));
            }

            question.IsDeleted = true;
            question.UpdatedAt = DateTime.UtcNow;

            await _questionRepository.UpdateAsync(question);
            await _questionRepository.SaveChangesAsync();

            return Result<bool>.Success(true);
        }
        private Question CreateQuestion(QuestionRequest request, Guid? previousQuestionId = null)
        {
            var now = DateTime.UtcNow;

            return new Question
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
                UpdatedAt = null,

                Choices = request.Choices
                    .Select(choiceRequest => new QuestionChoice
                    {
                        Id = Guid.NewGuid(),
                        Text = choiceRequest.Text,
                        IsCorrect = choiceRequest.IsCorrect,
                        CreatedAt = now,
                        UpdatedAt = null
                    })
                    .ToList()
            };
        }

    }
}

