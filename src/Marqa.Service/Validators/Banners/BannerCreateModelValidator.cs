using FluentValidation;
using Marqa.Service.Services.Banners.Models;

namespace Marqa.Service.Validators.Banners;

public class BannerCreateModelValidator : AbstractValidator<BannerCreateModel>
{
    public BannerCreateModelValidator()
    {
        RuleFor(x => x.CompanyId)
            .GreaterThan(0);

        RuleFor(x => x.Title).NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(500);

        RuleFor(x => x.DisplayOrder)
            .GreaterThanOrEqualTo(0);

        RuleFor(x => x.StartDate)
            .LessThan(x => x.EndDate);

        RuleFor(x => x.EndDate)
            .GreaterThan(DateTime.Now);
    }
}
