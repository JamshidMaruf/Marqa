using FluentValidation;
using Marqa.DataAccess.UnitOfWork;
using Marqa.Service.Services.Teachers.Models;

namespace Marqa.Service.Validators.Teachers;

public class TeacherUpdateModelValidator : AbstractValidator<TeacherUpdateModel>
{
    public TeacherUpdateModelValidator(IUnitOfWork unitOfWork)
    {
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
            .Must(BeAValidPastDate)
            .WithMessage("Date of birth must be a valid past date.");

        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("Phone number is required.")
            .Matches(@"^\+?\d{9,15}$").WithMessage("Invalid phone number format.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress();

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
            .WithMessage("Joining date cannot be a future date.");

        RuleFor(x => x.SubjectIds)
            .NotEmpty().WithMessage("At least one subject is required.")
            .Must(ids => ids.Distinct().Count() == ids.Count)
            .WithMessage("Duplicate subject IDs are not allowed.");

        RuleForEach(x => x.SubjectIds)
            .Must(subjectId => unitOfWork.Subjects.CheckExist(s => s.Id == subjectId)
            )
            .WithMessage("Subject with the given Id does not exist.");
    }

    private bool BeAValidPastDate(DateOnly dob)
    {
        return dob < DateOnly.FromDateTime(DateTime.UtcNow);
    }
    
}