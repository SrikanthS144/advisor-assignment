using Application.Advisors;
using FluentValidation;

namespace Application.FluentValidations
{
    public class UpdateAdvisorCommandValidator : AbstractValidator<UpdateAdvisorCommand>
    {
        public UpdateAdvisorCommandValidator()
        {
            RuleFor(command => command.AdvisorId)
                .GreaterThan(0)
                .WithMessage("AdvisorId must be greater than zero.");

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
                .WithMessage("SIN must be a 9-digit number.");

            RuleFor(advisor => advisor.Phone)
                .Matches(@"^\d{8}$")
                .WithMessage("Phone number must be exactly 8 digits.")
                .When(command => !string.IsNullOrEmpty(command.Phone));

            RuleFor(advisor => advisor.Address)
                .MaximumLength(255)
                .WithMessage("Name is less than 255 character.");

            RuleFor(advisor => advisor.HealthStatus)
                .Matches(@"^\d+$")
                .WithMessage("HealthStatus must be a numeric value.");
        }
    }
}
