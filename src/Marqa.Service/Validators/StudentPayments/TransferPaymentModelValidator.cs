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
        RuleFor(x => x.StudentId).Must(studentId => 
            unitOfWork.Students.Exist(s => s.Id == studentId))
            .WithMessage("Student does not exist.");
    }
}