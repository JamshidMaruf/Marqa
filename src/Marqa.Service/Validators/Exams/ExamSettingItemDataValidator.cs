using FluentValidation;
using Marqa.Service.Services.Exams.Models;

namespace Marqa.Service.Validators.Exams;

public class ExamSettingItemDataValidator : AbstractValidator<ExamCreateModel.ExamSettingItemData>
{
    public ExamSettingItemDataValidator()
    {
        RuleFor(t => t.Score).GreaterThan(1);
    }   
}