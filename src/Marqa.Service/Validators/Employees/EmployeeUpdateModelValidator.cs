using FluentValidation;
using Marqa.Service.Services.Employees.Models;

namespace Marqa.Service.Validators.Employees;
public class EmployeeUpdateModelValidator : AbstractValidator<EmployeeUpdateModel>
{
    public EmployeeUpdateModelValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .MaximumLength(50).WithMessage("First name must be at most 50 characters.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .MaximumLength(50).WithMessage("Last name must be at most 50 characters.");

        RuleFor(x => x.DateOfBirth)
            .LessThan(DateOnly.FromDateTime(DateTime.Today))
            .WithMessage("Date of Birth must be in the past.");

        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("Phone number is required.")
            .Matches(@"^\+998\d{9}$")
            .WithMessage("Phone number is invalid format.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid Email format.");

        RuleFor(x => x.Gender)
            .IsInEnum().WithMessage("Invalid gender value.");

        RuleFor(x => x.Status)
            .IsInEnum().WithMessage("Invalid status value.");

        RuleFor(x => x.JoiningDate)
            .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today))
            .WithMessage("Joining date cannot be in the future.");

        RuleFor(x => x.Specialization)
            .NotEmpty().WithMessage("Specialization is required.")
            .MaximumLength(100).WithMessage("Specialization must be at most 100 characters.");

        RuleFor(x => x.Info)
            .MaximumLength(500).WithMessage("Info must be at most 500 characters.");

        RuleFor(x => x.RoleId)
            .GreaterThan(0).WithMessage("RoleId is required.");
    }
}
