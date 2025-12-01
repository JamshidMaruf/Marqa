using FluentValidation;
using Marqa.DataAccess.UnitOfWork;
using Marqa.Service.Services.EmployeePayments.Models;

namespace Marqa.Service.Validators.EmployeePayments;

public class EmployeePaymentCreateModelValidator : AbstractValidator<EmployeePaymentCreateModel>
{
    public EmployeePaymentCreateModelValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(model => model.EmployeeId)
            .NotEmpty()
            .GreaterThan(0);
        RuleFor(model => model.EmployeeId).Must(e => unitOfWork.Employees.Exist(ex => ex.Id == e))
            .WithMessage("Employee not found.");

        RuleFor(model => model.PaymentMethod)
            .NotNull();

        RuleFor(model => model.EmployeePaymentOperationType)
            .NotNull();

        RuleFor(model => model.Amount)
            .NotEmpty()
            .Must(amount => amount > 0);

        RuleFor(model => model.DateTime)
            .NotEmpty()
            .Must(date => date.Date <= DateTime.UtcNow.Date);

        RuleFor(model => model.Description)
            .MaximumLength(500)
            .When(model => !string.IsNullOrWhiteSpace(model.Description));
    }
}