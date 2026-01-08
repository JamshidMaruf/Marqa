using FluentValidation;
using Marqa.Service.Services.Companies.Models;

namespace Marqa.Service.Validators.Companies;

public class CompanyUpdateModelValidator : AbstractValidator<CompanyUpdateModel>
{
    public CompanyUpdateModelValidator()
    {
        RuleFor(c => c.Name).NotEmpty().MaximumLength(255);

        RuleFor(c => c.Director).NotEmpty().MaximumLength(255);

        RuleFor(c => c.Address).NotEmpty();

        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("Phone number is required.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid Email format.");
    }
}
