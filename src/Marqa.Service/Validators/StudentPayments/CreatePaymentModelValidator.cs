using FluentValidation;

namespace Marqa.Service.Validators.StudentPaymentOperations;
public class CreatePaymentModelValidator : AbstractValidator<CreatePaymentModel>
{
    public CreatePaymentModelValidator()
    {
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

        RuleFor(x => x.PaymentOperationType)
            .Must(type => type == Domain.Enums.PaymentOperationType.Income ||
                         type == Domain.Enums.PaymentOperationType.Expense);
    }
}
