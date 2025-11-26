using FluentValidation;
using Marqa.Service.Services.EmployeeRoles.Models;

namespace Marqa.Service.Validators.EmployeeRoles
{
   public class EmployeeRoleUpdateModelValidator : AbstractValidator<EmployeeRoleUpdateModel>
   {
        public EmployeeRoleUpdateModelValidator()
        {
            RuleFor(e => e.Name).NotEmpty().MaximumLength(255);
        }
    }
}
