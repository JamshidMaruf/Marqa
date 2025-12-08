using FluentValidation;
using Marqa.DataAccess.UnitOfWork;
using Marqa.Domain.Enums;
using Marqa.Service.Services.Enrollments.Models;

namespace Marqa.Service.Validators.Enrollments;

public class EnrollmentCreateModelValidator : AbstractValidator<EnrollmentCreateModel>
{
    public EnrollmentCreateModelValidator()
    {
        RuleFor(x => x.StudentId)
            .NotEmpty().WithMessage("Student ID is required")
            .GreaterThan(0).WithMessage("Student ID must be greater than 0");

        RuleFor(x => x.CourseId)
            .NotEmpty().WithMessage("Course ID is required")
            .GreaterThan(0).WithMessage("Course ID must be greater than 0");

        RuleFor(x => x.EnrollmentDate)
            .NotEmpty().WithMessage("Enrollment date is required")
            .LessThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("Enrollment date cannot be in the future");

        RuleFor(x => x.PaymentType)
            .IsInEnum()
            .WithMessage("Invalid payment type");

        When(x => x.PaymentType == CoursePaymentType.DiscountInPercentage, () =>
        {
            RuleFor(x => x.Amount)
                .InclusiveBetween(0, 100)
                .WithMessage("Amount must be between 0 and 100 for percentage discount");
        });

        When(x => x.PaymentType == CoursePaymentType.Fixed, () =>
        {
            RuleFor(x => x.Amount)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Amount must be non-negative for fixed payment");
        });

        When(x => x.PaymentType == CoursePaymentType.DiscountFree, () =>
        {
            RuleFor(x => x.Amount)
                .Equal(0)
                .WithMessage("Amount must be 0 for discount-free payment type");
        });

        RuleFor(x => x.Amount)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Amount cannot be negative");
    }
}
