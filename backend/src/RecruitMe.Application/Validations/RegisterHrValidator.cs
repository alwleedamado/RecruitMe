using FluentValidation;
using RecruitMe.Application.DTOs;

namespace RecruitMe.Application.Validations;

public class RegisterHrValidator : AbstractValidator<RegisterHrRequest>
{
    public RegisterHrValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("Full name is required.")
            .MaximumLength(100).WithMessage("Full name cannot exceed 100 characters.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");

        RuleFor(x => x.Password).NotEmpty()
        .WithMessage("Password is required");

        RuleFor(x => x.Salary)
        .GreaterThan(0).WithMessage("Salary is positive decimal number");
    }
}
