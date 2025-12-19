using FluentValidation;
using Marqa.DataAccess.UnitOfWork;

namespace Marqa.Service.Services.StudentPayments.Validators;

public class UpdatePaymentModelValidator : AbstractValidator<UpdatePaymentModel>
{
    public UpdatePaymentModelValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(x => x.PaymentId)
            .GreaterThan(0);
        RuleFor(x => x.PaymentId).MustAsync(async (paymentId, cancellation) => await unitOfWork.StudentPaymentOperations.CheckExistAsync(p => p.Id == paymentId))
            .WithMessage("Payment does not exist.");

        RuleFor(x => x.StudentId)
            .GreaterThan(0).WithMessage("Student Id must be greater than 0.");
        RuleFor(x => x.StudentId).MustAsync(async (studentId, cancellation) => await unitOfWork.Companies.CheckExistAsync(p => p.Id == studentId))
            .WithMessage("Student does not exist.");

        RuleFor(x => x.CourseId)
            .GreaterThan(0).WithMessage("Course Id  must be greater than 0");
        RuleFor(x => x.CourseId).MustAsync(async (courseId, cancellation) => await unitOfWork.Companies.CheckExistAsync(p => p.Id == courseId))
            .WithMessage("Course does not exist.");

        RuleFor(x => x.Amount)
            .GreaterThan(0);

        RuleFor(x => x.CoursePrice)
            .GreaterThan(0);

        RuleFor(x => x.PaymentMethod)
            .IsInEnum().WithMessage("Invalid operation method!");

        RuleFor(x => x.PaymentOperationType)
            .IsInEnum().WithMessage("Invalid operation type!");

        RuleFor(x => x.Description).NotEmpty();

        RuleFor(x => x.PaymentOperationType)
            .Must(type => type == Domain.Enums.PaymentOperationType.Income ||
                         type == Domain.Enums.PaymentOperationType.Expense);
    }
}
