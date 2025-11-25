using FluentValidation;
using Marqa.Service.Services.Products.Models;

namespace Marqa.Service.Validators.Products
{
    public class ProductCreateModelValidator : AbstractValidator<ProductCreateModel>
    {
        public ProductCreateModelValidator()
        {
            RuleFor(p => p.CompanyId).NotNull().GreaterThan(0);
            RuleFor(p => p.Name).NotEmpty().Length(255);
            RuleFor(p => p.Price).NotNull();
        }
    }
}
