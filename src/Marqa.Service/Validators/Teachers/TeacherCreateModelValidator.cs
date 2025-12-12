using FluentValidation;
using Marqa.DataAccess.UnitOfWork;
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
            .Must(companyId=> unitOfWork.Companies.CheckExist(x => x.Id == companyId))
            .WithMessage("Company with given Id does not exist.");

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
            .Matches(@"^\+?\d{9,15}$").WithMessage("Invalid phone number format.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");

        RuleFor(x => x.Gender)
            .IsInEnum().WithMessage("Invalid gender value.");

        RuleFor(x => x.PaymentType)
            .IsInEnum().WithMessage("Invalid payment type.");

        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("Amount must be greater than zero.");

        RuleFor(x => x.Type)
            .IsInEnum().WithMessage("Invalid teacher type.");

        RuleFor(x => x.Status)
            .IsInEnum().WithMessage("Invalid teacher status.");

        RuleFor(x => x.JoiningDate)
            .NotEmpty().WithMessage("Joining date is required.")
            .Must(d => d <= DateOnly.FromDateTime(DateTime.UtcNow))
            .WithMessage("Joining date cannot be in the future.");

        RuleFor(x => x.SubjectIds)
            .NotEmpty().WithMessage("At least one subject is required.")
            .Must(list => list.All(id => id > 0))
            .WithMessage("Invalid subject ID.");
    
        RuleForEach(x => x.SubjectIds)
            .Must(subjectId => unitOfWork.Subjects.CheckExist(s => s.Id == subjectId)
            )
            .WithMessage("Subject with the given Id does not exist.");
    }

    private bool BeAValidAge(DateOnly dob)
    {
        return dob < DateOnly.FromDateTime(DateTime.UtcNow);
    }
    
}