using FluentValidation;
using Marqa.Service.Services.PointSettings.Models;

namespace Marqa.Service.Validators.PointSettings;

public class PointSettingUpdateModelValidator : AbstractValidator<PointSettingUpdateModel>
{
    public PointSettingUpdateModelValidator()
    {
        RuleFor(p => p.Name).NotNull().Length(100);
        RuleFor(p => p.Operation).NotNull();
        RuleFor(p => p.Point).NotNull().GreaterThan(0);
    }
}
