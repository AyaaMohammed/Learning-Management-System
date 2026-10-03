using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Results
{
    public static class Errors
    {
        public static class QuestionError
        {
            public static Error NotFound =>
                new(
                    "Question was not found.",
                    ErrorType.NotFound);

        }
        public static class QuizError
        {
            public static Error NotFound =>
                new(
                    "Quiz was not found.",
                    ErrorType.NotFound);

            public static Error Inactive =>
                new(
                    "Quiz is not active.",
                    ErrorType.Conflict);

            public static Error NoQuestions =>
                new(
                    "Quiz must contain at least one question.",
                    ErrorType.Validation);

            public static Error InvalidQuestions =>
                new(
                    "One or more questions are invalid.",
                    ErrorType.NotFound);

            public static Error MaximumAttemptsExceeded =>
                new(
                    "Maximum attempts exceeded.",
                    ErrorType.Conflict);

            public static Error AttemptNotFound =>
                new(
                    "Attempt not found.",
                    ErrorType.NotFound);

            public static Error AttemptAlreadySubmitted =>
                new(
                    "This attempt has already been submitted.",
                    ErrorType.Conflict);

            public static Error QuestionsNotFound =>
                new(
                    "Quiz has no questions.",
                    ErrorType.Validation);

            public static Error InvalidQuestion =>
                new(
                    "One or more questions do not belong to this quiz.",
                    ErrorType.Validation);

            public static Error DuplicateQuestionAnswer =>
                new(
                    "A question cannot be answered more than once.",
                    ErrorType.Validation);

            public static Error QuestionNotFound =>
                new(
                    "Question not found.",
                    ErrorType.NotFound);

            public static Error InvalidSelectedChoice =>
                new(
                    "Selected choice does not belong to the question.",
                    ErrorType.Validation);

            public static Error ConcurrencyConflict =>
                new(
                    "The quiz attempt was modified by another request.",
                    ErrorType.Conflict);
                    }

    }
}
