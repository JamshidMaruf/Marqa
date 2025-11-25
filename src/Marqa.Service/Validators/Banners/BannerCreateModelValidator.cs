using FluentValidation;
using Marqa.Service.Services.Banners.Models;

namespace Marqa.Service.Validators.Banners;

public class BannerCreateModelValidator : AbstractValidator<BannerCreateModel>
{
    public BannerCreateModelValidator()
    {
        RuleFor(x => x.CompanyId)
             .GreaterThan(0)
             .WithMessage("Company ID must be greater than 0");

        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("Title is required")
            .MaximumLength(255)
            .WithMessage("Title must not exceed 255 characters");

        RuleFor(x => x.LinkUrl)
            .NotEmpty()
            .WithMessage("Link URL is required");

        RuleFor(x => x.DisplayOrder)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Display order must be 0 or greater");

        RuleFor(x => x.StartDate)
            .NotEmpty()
            .WithMessage("Start date is required")
            .LessThan(x => x.EndDate)
            .WithMessage("Start date must be earlier than end date");

        RuleFor(x => x.EndDate)
            .NotEmpty()
            .WithMessage("End date is required")
            .GreaterThan(x => x.StartDate)
            .WithMessage("End date must be later than start date");
    }

}
