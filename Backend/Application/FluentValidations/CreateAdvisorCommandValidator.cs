using Application.Advisors;
using Domain.Data;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.FluentValidations
{
    public class CreateAdvisorCommandValidator : AbstractValidator<CreateAdvisorCommand>
    {
        private readonly AdvisorContext _advisorContext;
        public CreateAdvisorCommandValidator(AdvisorContext advisorContext)
        {
            _advisorContext = advisorContext;

            RuleFor(advisor => advisor.Name)
                .NotEmpty()
                .WithMessage("Name is required.")
                .MaximumLength(255)
                .WithMessage("Name is less than 255 character.");

            RuleFor(advisor => advisor.SIN)
                .NotEmpty()
                .WithMessage("SIN is required.")
                .Length(9)
                .WithMessage("SIN must be exactly 9 characters long.")
                .Matches(@"^\d{9}$")
                .WithMessage("SIN must be a 9-digit number.")
                .MustAsync(BeUniqueSIN).WithMessage("SIN number already exists.");

            RuleFor(advisor => advisor.Phone)
                .Matches(@"^\d{8}$")
                .WithMessage("Phone number must be exactly 8 digits.")
                .When(command => !string.IsNullOrEmpty(command.Phone));

            RuleFor(advisor => advisor.Address)
                .MaximumLength(255)
                .WithMessage("Name is less than 255 character.");

            RuleFor(advisor=>advisor.HealthStatus)
                .Matches(@"^\d+$")
                .WithMessage("HealthStatus must be a numeric value.");
        }

        private async Task<bool> BeUniqueSIN(string sin, CancellationToken cancellationToken)
        {
            return await _advisorContext.Advisor
                .AllAsync(a => a.Sin != sin, cancellationToken);
        }
    }
}

