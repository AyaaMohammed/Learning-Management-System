using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Questions
{
    public class QuestionResponseDto
    {
        public Guid Id { get; set; }

        public string Text { get; set; } = string.Empty;

        public string? ImageUrl { get; set; }

        public bool IsLocked { get; set; }

        public List<ChoiceResponseDto> Choices { get; set; } = new();
    }
    public class ChoiceResponseDto
    {
        public Guid Id { get; set; }

        public string Text { get; set; } = string.Empty;

        public bool IsCorrect { get; set; }
    }
}
