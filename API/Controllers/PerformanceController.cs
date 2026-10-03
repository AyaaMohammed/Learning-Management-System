using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/admin/performance")]
[ApiController]
[Authorize(Roles = "Admin")]
public class PerformanceController : ControllerBase
{
    private readonly IPerformanceService _performanceService;

    public PerformanceController(IPerformanceService performanceService)
    {
        _performanceService = performanceService;
    }
    // GET: api/admin/performance/students/{studentId}
    [HttpGet("students/{studentId:guid}")]
    public async Task<IActionResult> GetStudentPerformance(Guid studentId)
    {
        var result = await _performanceService.GetStudentPerformanceAsync(studentId);

        return result.ToActionResult(this);
    }
    // GET: api/admin/performance/students/{studentId}/questions
    [HttpGet("questions/{questionId:guid}")]
    public async Task<IActionResult> GetQuestionPerformance(Guid questionId)
    {
        var result = await _performanceService.GetQuestionPerformanceAsync(questionId);

        return result.ToActionResult(this);
    }

    // GET: api/admin/performance/questions/{questionId}/students
    [HttpGet("questions/{questionId:guid}/students")]
    public async Task<IActionResult>  GetStudentsPerformanceOnQuestion(Guid questionId)
    {
        var result = await _performanceService.GetStudentsPerformanceOnQuestionAsync(questionId);

        return result.ToActionResult(this);
    }
}