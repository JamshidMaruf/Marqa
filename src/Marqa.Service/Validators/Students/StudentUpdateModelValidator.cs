using FluentValidation;
using Marqa.Service.Services.Students.Models;

namespace Marqa.Service.Validators.Students;
public class StudentUpdateModelValidator : AbstractValidator<StudentUpdateModel>
{
    public StudentUpdateModelValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("FirstName is required")
            .MaximumLength(255).WithMessage("FirstName must not exceed 50 characters");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("LastName is required")
            .MaximumLength(255).WithMessage("LastName must not exceed 50 characters");

        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("Phone is required")
            .Matches(@"998\d{9}$").WithMessage("Phone number is not valid");

        RuleFor(x => x.Email)
            .EmailAddress()
            .When(x => !string.IsNullOrWhiteSpace(x.Email));

        RuleFor(x => x.DateOfBirth)
            .NotEmpty().WithMessage("DateOfBirth is required")
            .Must(BeValidAge).WithMessage("Student must be between 5 and 100 years old");

        RuleFor(x => x.StudentDetailUpdateModel)
            .NotNull().WithMessage("Student details are required")
            .SetValidator(new StudentDetailUpdateModelValidator());
    }

    private bool BeValidAge(DateOnly dateOfBirth)
    {
        var today = DateOnly.FromDateTime(DateTime.Today);
        var age = today.Year - dateOfBirth.Year;
        if (dateOfBirth > today.AddYears(-age))
            age--;
        return age >= 5 && age <= 100;
    }
}
