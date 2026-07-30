using FluentValidation;
using RecruitMe.Application.DTOs;

namespace RecruitMe.Application.Validations;

public class CreateHrValidator : AbstractValidator<RegisterHrRequest>
{
    public CreateHrValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty()
            .WithMessage("Full name is required.");
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("Email is invalid.");
        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Password is required");
        RuleFor(x => x.HireDate)
            .NotNull()
            .WithMessage("HireDate is required");
        RuleFor(x => x.Salary)
            .GreaterThan(0)
            .WithMessage("Salary must be greater than zero.");
    }
}
