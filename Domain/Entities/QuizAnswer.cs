using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class QuizAnswer
    {
        public Guid Id { get; set; }

        public Guid AttemptId { get; set; }

        public Guid QuestionId { get; set; }

        public Guid? SelectedChoiceId { get; set; }

        public bool IsCorrect { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public QuizAttempt Attempt { get; set; } = null!;

        public Question Question { get; set; } = null!;

        public QuestionChoice? SelectedChoice { get; set; }
    }
}
