using FluentValidation;
using Marqa.DataAccess.UnitOfWork;
using Marqa.Domain.Enums;
using Marqa.Service.Services.Enrollments.Models;

namespace Marqa.Service.Validators.Enrollments;

public class EnrollmentCreateModelValidator : AbstractValidator<EnrollmentCreateModel>
{
    public EnrollmentCreateModelValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(model => model.StudentId)
            .NotEmpty()
            .GreaterThan(0);

        RuleFor(model => model.StudentId)
            .Must(studentId => unitOfWork.Students.CheckExist(s => s.Id == studentId))
            .WithMessage("Student is not found.");

        RuleFor(model => model.CourseId)
            .NotEmpty()
            .GreaterThan(0);

        RuleFor(model => model.CourseId)
            .Must(courseId => unitOfWork.Courses.CheckExist(c => c.Id == courseId))
            .WithMessage("Course is not found.");

        RuleFor(model => model.EnrollmentDate)
            .NotEmpty()
            .Must(date => date.Date <= DateTime.UtcNow.Date)
            .WithMessage("Enrollment date cannot be in the future.");

        RuleFor(model => model.Status)
            .NotNull();

        RuleFor(model => model.PaymentType)
            .NotNull();

        RuleFor(model => model.Amount)
            .NotEmpty()
            .Must(amount => amount >= 0)
            .WithMessage("Amount must be greater than or equal to 0.");

        RuleFor(e => e.Amount)
           .Must((model, amount) => EnrollmentValidatorHelper.ValidateAmount(model, amount))
           .WithMessage(model => model.PaymentType == CoursePaymentType.DiscountInPercentage
               ? "Amount must be between 0 and 100 for discount"
               : "Amount must be positive");

        RuleFor(e => new { e.CourseId, e.StudentId })
             .MustAsync(async (x, cancellation) => await EnrollmentValidatorHelper.ValidateCourseCapacityAsync(unitOfWork, x.CourseId, x.StudentId))
             .WithMessage("This course has reached its maximum number of students.");
    }
}