
using API.Extensions;
using Application.DTOs.Questions;
using Application.Interfaces.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionService _questionService;
        public QuestionsController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        // GET: api/Questions
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _questionService.GetAllAsync();

            return result.ToActionResult(this);
        }

        // GET: api/Questions/{id}
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _questionService.GetByIdAsync(id);

            return result.ToActionResult(this);
        }

        // POST: api/Questions
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] QuestionRequest request)
        {
            var result = await _questionService.CreateAsync(request);

            return result.ToActionResult(this);
        }

        // PUT: api/Questions/{id}
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id,[FromBody] QuestionRequest request)
        {
            var result = await _questionService.UpdateAsync(id, request);

            return result.ToActionResult(this);
        }

        // DELETE: api/Questions/{id}
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _questionService.DeleteAsync(id);

            return result.ToActionResult(this);
        }
    }
}