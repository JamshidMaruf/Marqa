using FluentValidation;
using Marqa.DataAccess.UnitOfWork;
using Marqa.Service.DTOs.StudentPaymentOperations;

namespace Marqa.Service.Services.StudentPayments.Validators;
public class TransferPaymentModelValidator : AbstractValidator<TransferPaymentModel>
{
    public TransferPaymentModelValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(x => x.StudentId)
            .GreaterThan(0);
        RuleFor(x => x.StudentId).MustAsync(async (studentId, cancellation) => await unitOfWork.StudentPaymentOperations.CheckExistAsync(p => p.Id == studentId))
            .WithMessage("Student does not exist.");
    }
}