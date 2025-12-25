using FluentValidation;
using Marqa.Service.Services.Products.Models;

namespace Marqa.Service.Validators.Products
{
    public class ProductUpdateModelValidator : AbstractValidator<ProductUpdateModel>
    {
        public ProductUpdateModelValidator()
        {
            RuleFor(p => p.Name).NotEmpty().MaximumLength(255);
            RuleFor(p => p.Price).NotNull();
        }
    }
}
