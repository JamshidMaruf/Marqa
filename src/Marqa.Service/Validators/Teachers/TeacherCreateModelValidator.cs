using System.ComponentModel.DataAnnotations;
using FluentValidation;
using Marqa.Service.Services.Teachers.Models;

namespace Marqa.Service.Validators.Teachers;

public class TeacherCreateModelValidator : AbstractValidator<TeacherCreateModel>
{
    public TeacherCreateModelValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(x => x.CompanyId)
            .GreaterThan(0)
            .WithMessage("CompanyId is required.");
        RuleFor(x => x.CompanyId)
            .MustAsync(async (companyId, cancellation) => await unitOfWork.Companies.CheckExistAsync(p => p.Id == companyId))
            .WithMessage("Company not found");

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .MaximumLength(50);

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .MaximumLength(50);

        RuleFor(x => x.Qualification)
            .NotEmpty().WithMessage("Qualification is required.")
            .MaximumLength(200);

        RuleFor(x => x.Info)
            .MaximumLength(500)
            .When(x => !string.IsNullOrWhiteSpace(x.Info));

        RuleFor(x => x.DateOfBirth)
            .NotEmpty().WithMessage("Date of birth is required.")
            .Must(BeAValidAge).WithMessage("Date of birth must be in the past.");

        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("Phone number is required.")
            .Matches(@"^\d{9,15}$").WithMessage("Invalid phone number format.");

        RuleFor(x => x.Email)
            .EmailAddress()
            .When(x => !string.IsNullOrWhiteSpace(x.Email));

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");

        RuleFor(x => x.Gender)
            .IsInEnum().WithMessage("Invalid gender value.");

        RuleFor(x => x.PaymentType)
            .IsInEnum().WithMessage("Invalid payment type.");

        RuleFor(x => x.Type)
            .IsInEnum().WithMessage("Invalid teacher type.");

        RuleFor(x => x.Status)
            .IsInEnum().WithMessage("Invalid teacher status.");

        RuleFor(x => x.JoiningDate)
            .NotEmpty().WithMessage("Joining date is required.")
            .Must(d => d <= DateOnly.FromDateTime(DateTime.UtcNow))
            .WithMessage("Joining date cannot be in the future.");
    }

    private bool BeAValidAge(DateOnly dob)
    {
        return dob < DateOnly.FromDateTime(DateTime.UtcNow);
    }
    
}