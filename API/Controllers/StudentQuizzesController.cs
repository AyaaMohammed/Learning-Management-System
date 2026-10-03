using API.Extensions;
using Application.DTOs.Quizzes;
using Application.Interfaces.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/student/quizzes")]
[ApiController]
[Authorize(Roles = "Student")]
public class StudentQuizzesController : ControllerBase
{
    private readonly IQuizService _quizService;

    public StudentQuizzesController(IQuizService quizService)
    {
        _quizService = quizService;
    }

    [HttpGet("available")]
    public async Task<IActionResult> GetAvailable()
    {
        var result = await _quizService.GetAvailableForStudentAsync();

        return result.ToActionResult(this);
    }

    // POST: api/Quizzes/{quizId}/attempts
    [HttpPost("{quizId:guid}/attempts")]
    public async Task<IActionResult> StartQuiz(Guid quizId)
    {
        var result = await _quizService.StartQuizAsync(quizId);

        return result.ToActionResult(this);
    }

    // POST: api/Quizzes/attempts/{attemptId}/submit
    [HttpPost("attempts/{attemptId:guid}/submit")]
    public async Task<IActionResult> SubmitQuiz(Guid attemptId,[FromBody] SubmitQuizRequest request)
    {
        var result = await _quizService.SubmitQuizAsync(attemptId,request);
        return result.ToActionResult(this);
    }
}