using Application.DTOs.Questions;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators.Questions
{
    public class QuestionChoiceRequestValidator : AbstractValidator<QuestionChoiceRequest>
    {
        public QuestionChoiceRequestValidator()
        {
            RuleFor(x => x.Text)
                .NotEmpty()
                .WithMessage("Choice text is required.")
                .MaximumLength(200)
                .WithMessage("Choice text must not exceed 200 characters.");

            RuleFor(x => x.Text)
                .Must(text => text != null && text.Length > 0 && !string.IsNullOrWhiteSpace(text))
                .WithMessage("Choice text cannot be null, empty, or whitespace.");
        }
    }
}
