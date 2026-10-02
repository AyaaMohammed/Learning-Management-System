using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Quiz
    {
        public Guid Id { get; set; }

        public Guid TenantId { get; set; }

        public Guid CreatedByUserId { get; set; }

        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public bool IsActive { get; set; } = true;

        public int? MaxAttempts { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        public Tenant Tenant { get; set; } = null!;

        public User CreatedByUser { get; set; } = null!;

        public ICollection<QuizQuestion> QuizQuestions { get; set; } = new List<QuizQuestion>();

        public ICollection<QuizAttempt> Attempts { get; set; } = new List<QuizAttempt>();
    }
}
