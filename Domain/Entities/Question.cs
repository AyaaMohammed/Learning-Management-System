using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Question
    {
        public Guid Id { get; set; }

        public Guid TenantId { get; set; }

        public Guid CreatedByUserId { get; set; }

        public string Text { get; set; } = string.Empty;

        public string? ImageUrl { get; set; }

        public bool IsLocked { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        public Tenant Tenant { get; set; } = null!;

        public User CreatedByUser { get; set; } = null!;

        public ICollection<QuestionChoice> Choices { get; set; } = new List<QuestionChoice>();

        public ICollection<QuizQuestion> QuizQuestions { get; set; } = new List<QuizQuestion>();
    }    
    
}
