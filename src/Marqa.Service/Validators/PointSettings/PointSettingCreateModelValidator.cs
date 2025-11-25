using FluentValidation;
using Marqa.Service.Services.PointSettings.Models;

namespace Marqa.Service.Validators.PointSettings;
public class PointSettingCreateModelValidator : AbstractValidator<PointSettingCreateModel>
{
    public PointSettingCreateModelValidator()
    {
        RuleFor(p => p.Point).NotNull().NotEmpty().GreaterThan(0);
        RuleFor(p => p.Name).NotEmpty().Length(255);
        RuleFor(p => p.Operation).IsInEnum();
    }
}