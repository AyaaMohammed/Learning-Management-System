using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Quizzes
{
    public class SubmitQuizResponseDto
    {
        public Guid AttemptId { get; set; }
        public int Score { get; set; }
        public int TotalQuestions { get; set; }
        public decimal Percentage { get; set; }
        public DateTime SubmittedAt { get; set; }
    }
}
