using Application.DTOs.Performance;
using Application.Results;

namespace Application.Interfaces.Service;

public interface IPerformanceService
{

    Task<Result<StudentPerformanceResponseDto>> GetStudentPerformanceAsync(Guid studentId);
    Task<Result<QuestionPerformanceResponseDto>> GetQuestionPerformanceAsync(Guid questionId);
    Task<Result<StudentQuestionPerformanceResponseDto>> GetStudentsPerformanceOnQuestionAsync(Guid questionId);
}