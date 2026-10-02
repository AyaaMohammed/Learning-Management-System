using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Questions
{
    public class CreateQuestionRequest
    {
        public string Text { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
        public List<CreateQuestionChoiceRequest> Choices { get; set; } = new();
    }
}
