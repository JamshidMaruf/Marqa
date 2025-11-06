using FluentValidation;
using Marqa.Service.Services.Companies.Models;

namespace Marqa.Service.Validators.Companies;

public class CompanyUpdateModelValidator : AbstractValidator<CompanyUpdateModel>
{
    public CompanyUpdateModelValidator()
    {
        RuleFor(c => c.Name).NotNull().NotEmpty();
    }
}
