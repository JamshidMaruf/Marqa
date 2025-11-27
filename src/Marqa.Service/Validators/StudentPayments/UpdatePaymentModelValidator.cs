using FluentValidation;

namespace Marqa.Service.Services.StudentPayments.Validators;

public class UpdatePaymentModelValidator : AbstractValidator<UpdatePaymentModel>
{
    public UpdatePaymentModelValidator()
    {
        RuleFor(x => x.PaymentId)
            .GreaterThan(0);

        RuleFor(x => x.StudentId)
            .GreaterThan(0);

        RuleFor(x => x.CourseId)
            .GreaterThan(0);

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
