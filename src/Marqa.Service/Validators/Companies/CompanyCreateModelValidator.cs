using FluentValidation;
using Marqa.DataAccess.UnitOfWork;
using Marqa.Service.Services.Companies.Models;

namespace Marqa.Service.Validators.Companies;

public class CompanyCreateModelValidator : AbstractValidator<CompanyCreateModel>
{
    public CompanyCreateModelValidator()
    {
        RuleFor(c => c.Name).NotEmpty().MaximumLength(255);
        
        RuleFor(c => c.Director).NotEmpty().MaximumLength(500);
        
        RuleFor(c => c.Address).NotEmpty();
        
        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("Phone number is required.")
            .Matches(@"^998\d{9}$")
            .WithMessage("Phone number is invalid format.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid Email format.");
    }
}
