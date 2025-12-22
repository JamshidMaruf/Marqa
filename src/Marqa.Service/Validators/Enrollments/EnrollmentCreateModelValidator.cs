using FluentValidation;
using Marqa.Domain.Enums;
using Marqa.Service.Services.Enrollments.Models;

namespace Marqa.Service.Validators.Enrollments;

public class EnrollmentCreateModelValidator : AbstractValidator<EnrollmentCreateModel>
{
    public EnrollmentCreateModelValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(model => model.StudentId).GreaterThan(0).WithMessage("Student id must be greater than zero.");
        
        RuleFor(model => model.StudentId)
            .MustAsync(async (studentId, cancellation) => await unitOfWork.Students.CheckExistAsync(s => s.Id == studentId))
            .WithMessage("Student is not found.");

        RuleFor(model => model.CourseId)
           .MustAsync(async (courseId, cancellation) => await unitOfWork.Courses.CheckExistAsync(c => c.Id == courseId))
            .WithMessage("Course is not found.");

        RuleFor(model => model.EnrollmentDate)
            .NotEmpty()
            .Must(date => date.Date <= DateTime.UtcNow.Date)
            .WithMessage("Enrollment date cannot be in the future.");

        RuleFor(model => model.Status)
            .NotNull();

        RuleFor(model => model.PaymentType)
            .NotNull();
        
        RuleFor(e => e.Amount)
           .Must(EnrollmentValidatorHelper.ValidateAmount)
           .WithMessage(model => model.PaymentType == CoursePaymentType.DiscountInPercentage
               ? "Amount must be between 0 and 100 for discount"
               : "Amount must be positive");

        RuleFor(e => new { e.CourseId, e.StudentId })
             .MustAsync(async (x, cancellation) => !await unitOfWork.Enrollments.CheckExistAsync(e =>
                e.StudentId == x.StudentId &&
                 e.CourseId == x.CourseId))
             .WithMessage("Student already enrolled to this course.");
        
        RuleFor(e => new { e.CourseId, e.StudentId })
             .MustAsync(async (x, cancellation) => await EnrollmentValidatorHelper.ValidateCourseCapacityAsync(unitOfWork, x.CourseId, x.StudentId))
             .WithMessage("This course has reached its maximum number of students.");
    }
}