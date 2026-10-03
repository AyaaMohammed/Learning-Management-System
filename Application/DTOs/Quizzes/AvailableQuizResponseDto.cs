using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Quizzes
{
    public class AvailableQuizResponseDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public int? MaxAttempts { get; set; }

        public int AttemptsUsed { get; set; }

        public int? AttemptsRemaining { get; set; }

        public int TotalQuestions { get; set; }
    }
}
