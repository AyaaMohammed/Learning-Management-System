using Application.DTOs.Questions;
using Application.Validators;
using Application.Validators.Questions;
using FluentValidation;
using System;
using static System.Net.Mime.MediaTypeNames;

namespace Application.Validators.Questions
{
    public class QuestionRequestValidator : AbstractValidator<QuestionRequest>
    {
        public QuestionRequestValidator()
        {
            RuleFor(x => x.Text)
                .NotEmpty()
                .WithMessage("Question text is required.")
                .MaximumLength(500)
                .WithMessage("Question text must not exceed 500 characters.");

            RuleFor(x => x.ImageUrl)
                .Must(url => string.IsNullOrWhiteSpace(url) ||
                             Uri.TryCreate(url, UriKind.Absolute, out var uri) &&
                             (uri.Scheme == Uri.UriSchemeHttp ||
                              uri.Scheme == Uri.UriSchemeHttps))
                .WithMessage("Image URL must be a valid HTTP or HTTPS URL.");

            RuleFor(x => x.Choices)
                .NotNull()
                .WithMessage("Choices are required.")
                .Must(choices => choices != null && choices.Count >= 2)
                .WithMessage("A question must have at least 2 choices.")
                .Must(choices => choices != null && choices.Count(c => c.IsCorrect) == 1)
                .WithMessage("A question must have exactly one correct choice.");

            RuleFor(x => x.Choices)
                .Must(choices => choices != null && choices.Select(c => c.Text).Distinct().Count() == choices.Count)
                .WithMessage("Choice texts must be unique.");

            RuleForEach(x => x.Choices)
                .SetValidator(new QuestionChoiceRequestValidator());

        }
    }
}


