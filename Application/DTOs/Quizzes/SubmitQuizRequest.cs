using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Quizzes
{
    public class SubmitQuizRequest
    {
        public List<SubmitAnswerRequest> Answers { get; set; } = new();
    }
    public class SubmitAnswerRequest
    {
        public Guid QuestionId { get; set; }
        public Guid? SelectedChoiceId { get; set; }
    }
}
