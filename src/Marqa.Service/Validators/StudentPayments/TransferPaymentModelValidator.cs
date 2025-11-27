using FluentValidation;
using Marqa.Service.DTOs.StudentPaymentOperations;

namespace Marqa.Service.Services.StudentPayments.Validators;
public class TransferPaymentModelValidator : AbstractValidator<TransferPaymentModel>
{
    public TransferPaymentModelValidator()
    {
        RuleFor(x => x.StudentId)
            .GreaterThan(0);
    }
}