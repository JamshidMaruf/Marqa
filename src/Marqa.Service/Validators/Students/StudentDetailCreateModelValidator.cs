using FluentValidation;
using Marqa.Service.Services.Students.Models.DetailModels;

namespace Marqa.Service.Validators.Students;
public class StudentDetailCreateModelValidator : AbstractValidator<StudentDetailCreateModel>
{
    public StudentDetailCreateModelValidator()
    {
        RuleFor(x => x.FatherFirstName)
            .MaximumLength(255).WithMessage("Father's FirstName must not exceed 255 characters");

        RuleFor(x => x.FatherLastName)
            .MaximumLength(255).WithMessage("Father's LastName must not exceed 255 characters");


        RuleFor(x => x.MotherFirstName)
            .MaximumLength(255).WithMessage("Mother's FirstName must not exceed 255 characters");

        RuleFor(x => x.MotherLastName)
            .MaximumLength(255).WithMessage("Mother's LastName must not exceed 255 characters");


        RuleFor(x => x.GuardianFirstName)
            .MaximumLength(255).WithMessage("Guardian's FirstName must not exceed 50 characters");

        RuleFor(x => x.GuardianLastName)
            .MaximumLength(255).WithMessage("Guardian's LastName must not exceed 50 characters");


        RuleFor(x => x)
            .Must(x => !string.IsNullOrEmpty(x.FatherPhone) ||
                      !string.IsNullOrEmpty(x.MotherPhone) ||
                      !string.IsNullOrEmpty(x.GuardianPhone))
            .WithMessage("At least one parent/guardian phone number must be provided");
    }
}


