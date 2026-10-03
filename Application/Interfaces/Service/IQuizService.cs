using Application.DTOs.Quizzes;
using Application.Results;

namespace Application.Interfaces.Service
{
    public interface IQuizService
    {
        Task<Result<Guid>> CreateAsync(QuizRequest request);
        Task<Result<IReadOnlyList<AvailableQuizResponseDto>>> GetAvailableForStudentAsync();
        Task<Result<StartQuizResponseDto>> StartQuizAsync(Guid quizId);
        Task<Result<SubmitQuizResponseDto>> SubmitQuizAsync(Guid attemptId, SubmitQuizRequest request);
    }
}

