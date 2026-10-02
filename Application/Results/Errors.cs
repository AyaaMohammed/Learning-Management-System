using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Results
{
    public static class Errors
    {
        public static class Question
        {
            public static Error NotFound =>
                new(
                    "Question.NotFound",
                    "Question was not found.",
                    ErrorType.NotFound);

            public static Error Locked =>
                new(
                    "Question.Locked",
                    "Question cannot be modified because it is locked.",
                    ErrorType.Conflict);

            public static Error InvalidChoices =>
                new(
                    "Question.InvalidChoices",
                    "Question must have at least two choices and exactly one correct choice.",
                    ErrorType.Validation);
        }

        public static class Quiz
        {
            public static Error NotFound =>
                new(
                    "Quiz.NotFound",
                    "Quiz was not found.",
                    ErrorType.NotFound);

            public static Error Inactive =>
                new(
                    "Quiz.Inactive",
                    "Quiz is not active.",
                    ErrorType.Conflict);

            public static Error NoQuestions =>
                new(
                    "Quiz.NoQuestions",
                    "Quiz must contain at least one question.",
                    ErrorType.Validation);
        }

        public static class Attempt
        {
            public static Error NotFound =>
                new(
                    "Attempt.NotFound",
                    "Attempt was not found.",
                    ErrorType.NotFound);

            public static Error AlreadySubmitted =>
                new(
                    "Attempt.AlreadySubmitted",
                    "This attempt has already been submitted.",
                    ErrorType.Conflict);
        }
    }
}
