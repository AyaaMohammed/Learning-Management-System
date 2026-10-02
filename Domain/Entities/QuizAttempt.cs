using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class QuizAttempt
    {
        public Guid Id { get; set; }

        public Guid TenantId { get; set; }

        public Guid QuizId { get; set; }

        public Guid StudentId { get; set; }

        public AttemptStatus Status { get; set; } = AttemptStatus.InProgress;

        public int Score { get; set; }

        public int TotalQuestions { get; set; }

        public decimal Percentage { get; set; }

        public DateTime StartedAt { get; set; } = DateTime.UtcNow;

        public DateTime? SubmittedAt { get; set; }

        public byte[] RowVersion { get; set; } = null!;

        public Tenant Tenant { get; set; } = null!;

        public Quiz Quiz { get; set; } = null!;

        public User Student { get; set; } = null!;

        public ICollection<QuizAnswer> Answers { get; set; } = new List<QuizAnswer>();
    }
}
