using FluentValidation;
using Marqa.DataAccess.UnitOfWork;
using Marqa.Service.Services.EmployeeRoles.Models;

namespace Marqa.Service.Validators.EmployeeRoles;

public class EmployeeRoleCreateModelValidator : AbstractValidator<EmployeeRoleCreateModel>
{
    public EmployeeRoleCreateModelValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(e => e.CompanyId).NotNull().GreaterThan(0);
        RuleFor(e => e.CompanyId)
            .MustAsync(async (companyId, cancellationToken) =>
            {
                return await unitOfWork.Companies.CheckExistAsync(c => c.Id == companyId);
            })
            .WithMessage("Company with given Id does not exist.");
        RuleFor(e => e.Name).NotEmpty().MaximumLength(255);
    }
}
