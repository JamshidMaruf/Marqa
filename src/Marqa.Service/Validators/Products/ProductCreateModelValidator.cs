using FluentValidation;
using Marqa.DataAccess.UnitOfWork;
using Marqa.Service.Services.Products.Models;

namespace Marqa.Service.Validators.Products
{
    public class ProductCreateModelValidator : AbstractValidator<ProductCreateModel>
    {
        public ProductCreateModelValidator(IUnitOfWork unitOfWork)
        {
            RuleFor(p => p.CompanyId).NotNull().GreaterThan(0);
            RuleFor(p => p.CompanyId).MustAsync(async (companyId, cancellation) => await unitOfWork.Companies.CheckExistAsync(c => c.Id == companyId))
                .WithMessage("Company does not exist.");
            RuleFor(p => p.Name).NotEmpty().Length(255);
            RuleFor(p => p.Price).NotNull();
        }
    }
}
