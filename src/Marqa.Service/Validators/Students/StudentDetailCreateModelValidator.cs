using FluentValidation;
using Marqa.Service.Services.Students.Models.DetailModels;
namespace Marqa.Service.Validators.Banners;

public class StudentDetailCreateModelValidator : AbstractValidator<StudentDetailCreateModel>
{
    public StudentDetailCreateModelValidator()
    {
        RuleFor(x => x.FatherFirstName)
            .MaximumLength(50).WithMessage("Father's FirstName must not exceed 50 characters");

        RuleFor(x => x.FatherLastName)
            .MaximumLength(50).WithMessage("Father's LastName must not exceed 50 characters");

            RuleFor(x => x.FatherPhone).
            Matches(@"^\+998\d{9}$").WithMessage("Father's phone number is not valid");





        RuleFor(x => x.MotherFirstName)
            .MaximumLength(50).WithMessage("Mother's FirstName must not exceed 50 characters");

        RuleFor(x => x.MotherLastName)
            .MaximumLength(50).WithMessage("Mother's LastName must not exceed 50 characters");

            RuleFor(x => x.MotherPhone)
                .Matches(@"^\+998\d{9}$").WithMessage("Mother's phone number is not valid");




        RuleFor(x => x.GuardianFirstName)
            .MaximumLength(50).WithMessage("Guardian's FirstName must not exceed 50 characters");

        RuleFor(x => x.GuardianLastName)
            .MaximumLength(50).WithMessage("Guardian's LastName must not exceed 50 characters");

            RuleFor(x => x.GuardianPhone)
                .Matches(@"^\+998\d{9}$").WithMessage("Guardian's phone number is not valid");

        RuleFor(x => x)
            .Must(x => !string.IsNullOrEmpty(x.FatherPhone) ||
                      !string.IsNullOrEmpty(x.MotherPhone) ||
                      !string.IsNullOrEmpty(x.GuardianPhone))
            .WithMessage("At least one parent/guardian phone number must be provided");
    }
}
