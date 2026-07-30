using FluentValidation;
using FluentValidation.Validators;
using RecruitMe.Application.DTOs;
using RecruitMe.Domain.Entities;

namespace RecruitMe.Application.Validations;

public class CreateJobPostingValidator : AbstractValidator<CreateJobPosting>
{
    public CreateJobPostingValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("Title is required");
        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Description is required");
        RuleFor(x => x.Location)
            .NotEmpty()
            .When(x => x.JobType is JobType.OnSite or JobType.Hybrid)
            .WithMessage("Location is required when specifying onsite or hybrid job type");
        RuleFor(x => x.Requirements)
            .NotEmpty()
            .WithMessage("Requirements is required");
        RuleFor(x => x.EndsOn)
            .NotEmpty()
            .WithMessage("EndsOn is required")
            .GreaterThan(DateTime.Now)
            .WithMessage("EndsOn must be in the future");
    }
}
