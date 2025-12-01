using FluentValidation;
using Marqa.DataAccess.UnitOfWork;

namespace Marqa.Service.Services.StudentPayments.Validators;

public class CancelPaymentModelValidator : AbstractValidator<CancelPaymentModel>
{
    public CancelPaymentModelValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(x => x.PaymentId)
            .GreaterThan(0);
        RuleFor(x => x.PaymentId).Must(paymentId => 
            unitOfWork.StudentPaymentOperations.Exist(p => p.Id == paymentId))
            .WithMessage("Payment does not exist.");

        RuleFor(x => x.Description)
            .NotEmpty();
    }
}

