using FluentValidation;

namespace Marqa.Service.Services.StudentPayments.Validators;

public class CancelPaymentModelValidator : AbstractValidator<CancelPaymentModel>
{
    public CancelPaymentModelValidator()
    {
        RuleFor(x => x.PaymentId)
            .GreaterThan(0);

        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(500);
    }
}

