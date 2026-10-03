using API.Extensions;
using Application.DTOs.Quizzes;
using Application.Interfaces.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class QuizzesController : ControllerBase
    {
        private readonly IQuizService _quizService;

        public QuizzesController(IQuizService quizService)
        {
            _quizService = quizService;
        }

        // POST: api/Quizzes
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] QuizRequest request)
        {
            var result = await _quizService.CreateAsync(request);

            return result.ToActionResult(this);
        }
    }
}

