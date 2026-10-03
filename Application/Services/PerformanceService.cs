using Application.DTOs.Performance;
using Application.Interfaces.Service;
using Application.Interfaces.UnitOfWork;
using Application.Interfaces.UserService;
using Application.Results;
using Domain.Enums;

namespace Application.Services
{
    public class PerformanceService : IPerformanceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;

        public PerformanceService(IUnitOfWork unitOfWork,IUserService userService)
        {
            _unitOfWork = unitOfWork;
            _userService = userService;
        }
        public async Task<Result<StudentPerformanceResponseDto>> GetStudentPerformanceAsync(Guid studentId)
        {
            var tenantId = _userService.TenantId;

            var student = await _unitOfWork.Users.FirstOrDefaultAsync(
                x => x.Id == studentId &&  x.TenantId == tenantId);

            if (student is null)
            {
                return Result<StudentPerformanceResponseDto>.Failure(
                    new Error(
                        "Student not found.",
                        ErrorType.NotFound));
            }

            var attempts = await _unitOfWork.QuizAttempts.GetAllAsync(
                x => x.StudentId == studentId &&
                     x.TenantId == tenantId);

            var completedAttempts = attempts
                .Where(x => x.Status == AttemptStatus.Submitted)
                .ToList();

            var averageScore = completedAttempts.Any()
                ? (decimal)completedAttempts.Average(x => x.Score)
                : 0;

            var averagePercentage = completedAttempts.Any()
                ? completedAttempts.Average(x => x.Percentage)
                : 0;

            return Result<StudentPerformanceResponseDto>.Success(
                new StudentPerformanceResponseDto
                {
                    StudentId = student.Id,
                    StudentName = student.Name,
                    TotalAttempts = attempts.Count,
                    CompletedAttempts = completedAttempts.Count,
                    AverageScore = Math.Round(averageScore, 2),
                    AveragePercentage = Math.Round(averagePercentage, 2)
                });
        }

        public async Task<Result<QuestionPerformanceResponseDto>> GetQuestionPerformanceAsync(Guid questionId)
        {
            var tenantId = _userService.TenantId;

            var question = await _unitOfWork.Questions.FirstOrDefaultAsync(
                x => x.Id == questionId && x.TenantId == tenantId && !x.IsDeleted);

            if (question is null)
            {
                return Result<QuestionPerformanceResponseDto>.Failure(
                    new Error(
                        "Question not found.",
                        ErrorType.NotFound));
            }

            var answers = await _unitOfWork.QuizAnswers.GetAllAsync(
                x => x.QuestionId == questionId &&
                     x.Attempt.TenantId == tenantId &&
                     x.Attempt.Status == AttemptStatus.Submitted);

            var totalAnswers = answers.Count;
            var correctAnswers = answers.Count(x => x.IsCorrect);
            var wrongAnswers = totalAnswers - correctAnswers;

            var successRate = totalAnswers == 0
                ? 0
                : Math.Round(
                    (decimal)correctAnswers / totalAnswers * 100,
                    2);

            return Result<QuestionPerformanceResponseDto>.Success(
                new QuestionPerformanceResponseDto
                {
                    QuestionId = question.Id,
                    QuestionText = question.Text,
                    TotalAnswers = totalAnswers,
                    CorrectAnswers = correctAnswers,
                    WrongAnswers = wrongAnswers,
                    SuccessRate = successRate
                });
        }       
        
        public async Task<Result<StudentQuestionPerformanceResponseDto>> GetStudentsPerformanceOnQuestionAsync(Guid questionId)
        {
            var tenantId = _userService.TenantId;

            var question = await _unitOfWork.Questions.FirstOrDefaultAsync(
                x => x.Id == questionId &&
                     x.TenantId == tenantId &&
                     !x.IsDeleted);

            if (question is null)
            {
                return Result<StudentQuestionPerformanceResponseDto>.Failure(
                    new Error(
                        "Question not found.",
                        ErrorType.NotFound));
            }

            var answers = await _unitOfWork.QuizAnswers.GetAllAsync(
                x => x.QuestionId == questionId &&
                     x.Attempt.TenantId == tenantId &&
                     x.Attempt.Status == AttemptStatus.Submitted,
                x => x.Attempt);

            var studentIds = answers
                .Select(x => x.Attempt.StudentId)
                .Distinct()
                .ToList();

            var students = await _unitOfWork.Users.GetAllAsync(
                x => studentIds.Contains(x.Id) &&
                     x.TenantId == tenantId);

            var result = answers
                .Select(answer =>
                {
                    var student = students.First(x => x.Id == answer.Attempt.StudentId);

                    return new StudentQuestionResultDto
                    {
                        StudentId = student.Id,
                        StudentName = student.Name,
                        SelectedChoiceId = answer.SelectedChoiceId,
                        IsCorrect = answer.IsCorrect
                    };
                })
                .ToList();

            return Result<StudentQuestionPerformanceResponseDto>.Success(
                new StudentQuestionPerformanceResponseDto
                {
                    QuestionId = question.Id,
                    QuestionText = question.Text,
                    Students = result
                });
        }
    }
}
