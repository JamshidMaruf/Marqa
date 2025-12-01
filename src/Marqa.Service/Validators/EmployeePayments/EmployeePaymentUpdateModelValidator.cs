using FluentValidation;
using Marqa.DataAccess.UnitOfWork;
using Marqa.Service.Services.EmployeePayments.Models;

namespace Marqa.Service.Validators.EmployeePayments;

public class EmployeePaymentUpdateModelValidator : AbstractValidator<EmployeePaymentUpdateModel>
{
    public EmployeePaymentUpdateModelValidator(IUnitOfWork unitOfWork)
    {
        Include(new EmployeePaymentCreateModelValidator(unitOfWork));

        RuleFor(model => model.Id)
            .NotEmpty()
            .GreaterThan(0);
    }
}