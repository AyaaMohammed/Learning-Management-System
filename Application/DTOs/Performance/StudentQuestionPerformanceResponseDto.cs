using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Performance
{
    public class StudentQuestionPerformanceResponseDto
    {
        public Guid QuestionId { get; set; }
        public string QuestionText { get; set; } = string.Empty;
        public List<StudentQuestionResultDto> Students { get; set; } = new();
    }

    public class StudentQuestionResultDto
    {
        public Guid StudentId { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public Guid? SelectedChoiceId { get; set; }
        public bool IsCorrect { get; set; }
    }
}
