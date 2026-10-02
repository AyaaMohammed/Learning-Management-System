using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class QuizQuestion
    {
        public Guid QuizId { get; set; }

        public Guid QuestionId { get; set; }

        public int? DisplayOrder { get; set; }

        public Quiz Quiz { get; set; } = null!;

        public Question Question { get; set; } = null!;
    }
}
