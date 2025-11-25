using FluentValidation;
using Marqa.Service.Services.EmployeeRoles.Models;

namespace Marqa.Service.Validators.EmployeeRoles;

public class EmployeeRoleCreateModelValidator : AbstractValidator<EmployeeRoleCreateModel>
{
    public EmployeeRoleCreateModelValidator()
    {
        RuleFor(e => e.CompanyId).NotNull().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(255);
    }
}
