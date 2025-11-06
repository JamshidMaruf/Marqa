using FluentValidation;
using Marqa.Service.Services.Banners.Models;

namespace Marqa.Service.Validators.Banners;

public class BannerCreateModelValidator : AbstractValidator<BannerCreateModel>
{
    public BannerCreateModelValidator()
    {
        RuleFor(b => b.CompanyId)
            .LessThan(5)
            .WithMessage("CompanyId is required")
            .GreaterThan(0)
            .WithMessage("CompanyId kiritilishi kerak");
        
        RuleFor(b => b.Title).NotNull();
        RuleFor(b => b.Image).NotNull();
    }
}