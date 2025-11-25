using FluentValidation;
using Marqa.Service.Services.PointSettings.Models;

namespace Marqa.Service.Validators.PointSettings;

public class PointSettingUpdateModelValidator : AbstractValidator<PointSettingUpdateModel>
{
    public PointSettingUpdateModelValidator()
    {
        RuleFor(p => p.Name).NotEmpty().Length(255);
        RuleFor(p => p.Operation).IsInEnum();
        RuleFor(p => p.Point).NotNull().GreaterThan(0);
    }
}
