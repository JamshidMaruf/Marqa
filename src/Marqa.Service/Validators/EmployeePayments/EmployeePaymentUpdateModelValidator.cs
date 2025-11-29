using FluentValidation;
using Marqa.Service.Services.EmployeePayments.Models;

namespace Marqa.Service.Validators.EmployeePayments;

public class EmployeePaymentUpdateModelValidator : AbstractValidator<EmployeePaymentUpdateModel>
{
    public EmployeePaymentUpdateModelValidator()
    {
        Include(new EmployeePaymentCreateModelValidator());

        RuleFor(model => model.Id)
            .NotEmpty()
            .GreaterThan(0);
    }
}