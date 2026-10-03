using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Quizzes
{
    public class StartQuizResponseDto
    {
        public Guid AttemptId { get; set; }
        public Guid QuizId { get; set; }
        public string Title { get; set; } = string.Empty;
        public int TotalQuestions { get; set; }
        public List<QuizQuestionResponseDto> Questions { get; set; } = new();
    }

    public class QuizQuestionResponseDto
    {
        public Guid QuestionId { get; set; }
        public string Text { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
        public int? DisplayOrder { get; set; }
        public List<QuizChoiceResponseDto> Choices { get; set; } = new();
    }

    public class QuizChoiceResponseDto
    {
        public Guid Id { get; set; }
        public string Text { get; set; } = string.Empty;
    }
}
