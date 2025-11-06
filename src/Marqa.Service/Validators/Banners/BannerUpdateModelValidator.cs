using FluentValidation;
using Marqa.Service.Services.Banners.Models;

namespace Marqa.Service.Validators.Banners;

namespace Marqa.Service.Validators.Banners;

public class BannerUpdateModelValidator : AbstractValidator<BannerUpdateModel>
{
    public BannerUpdateModelValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("Title is required")
            .MaximumLength(255)
            .WithMessage("Title must not exceed 200 characters");

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Description is required")
            .MaximumLength(1000)
            .WithMessage("Description must not exceed 1000 characters");

        RuleFor(x => x.LinkUrl)
            .NotEmpty()
            .WithMessage("Link URL is required")
            .MaximumLength(255)
            .WithMessage("Link URL must not exceed 500 characters");

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
