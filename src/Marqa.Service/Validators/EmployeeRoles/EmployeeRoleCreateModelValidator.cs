using FluentValidation;
using Marqa.DataAccess.UnitOfWork;
using Marqa.Service.Services.EmployeeRoles.Models;

namespace Marqa.Service.Validators.EmployeeRoles;

public class EmployeeRoleCreateModelValidator : AbstractValidator<EmployeeRoleCreateModel>
{
    public EmployeeRoleCreateModelValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(e => e.CompanyId).NotNull().GreaterThan(0);
        RuleFor(e => e.CompanyId).Must(e => unitOfWork.Companies.Exist(ex => ex.Id == e))
            .WithMessage("Company with given Id does not exist.");
        RuleFor(e => e.Name).NotEmpty().MaximumLength(255);
    }
}
