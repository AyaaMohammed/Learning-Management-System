using Application.DTOs.Quizzes;
using FluentValidation;

namespace Application.Validators.Quizzes
{
    public class QuizRequestValidator : AbstractValidator<QuizRequest>
    {
        public QuizRequestValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Quiz title is required.")
                .MaximumLength(200)
                .WithMessage("Quiz title cannot exceed 200 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(1000)
                .When(x => !string.IsNullOrWhiteSpace(x.Description))
                .WithMessage("Quiz description cannot exceed 1000 characters.");

            RuleFor(x => x.MaxAttempts)
                .GreaterThan(0)
                .When(x => x.MaxAttempts.HasValue)
                .WithMessage("MaxAttempts must be greater than 0.");

            RuleFor(x => x.QuestionIds)
                .NotEmpty()
                .WithMessage("Quiz must contain at least one question.");

            RuleFor(x => x.QuestionIds)
                .Must(x => x.Distinct().Count() == x.Count)
                .WithMessage("Quiz cannot contain duplicate questions.");
        }
    }
}
