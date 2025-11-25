using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Marqa.Service.Services.Settings.Models;

namespace Marqa.Service.Validators.Settings;
public class SettingCreateModelValidator : AbstractValidator<SettingCreateModel>
{
    public SettingCreateModelValidator()
    {
        RuleFor(x => x.Key)
            .NotEmpty().WithMessage("Key is required.");
        RuleFor(x => x.Value)
            .NotEmpty().WithMessage("Value is required.");
        RuleFor(x => x.Category)
            .NotEmpty().WithMessage("Category is required.");
    }
}
