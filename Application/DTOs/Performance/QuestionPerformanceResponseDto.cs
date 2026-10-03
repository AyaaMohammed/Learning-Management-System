using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Performance
{
    public class QuestionPerformanceResponseDto
    {
        public Guid QuestionId { get; set; }
        public string QuestionText { get; set; } = string.Empty;

        public int TotalAnswers { get; set; }
        public int CorrectAnswers { get; set; }
        public int WrongAnswers { get; set; }

        public decimal SuccessRate { get; set; }
    }
}
