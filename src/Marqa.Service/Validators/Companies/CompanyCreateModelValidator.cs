using FluentValidation;
using Marqa.Service.Services.Companies.Models;

namespace Marqa.Service.Validators.Companies;

public class CompanyCreateModelValidator : AbstractValidator<CompanyCreateModel>
{
    public CompanyCreateModelValidator()
    {
        RuleFor(c => c.Name).NotNull().NotEmpty();
    }
}
