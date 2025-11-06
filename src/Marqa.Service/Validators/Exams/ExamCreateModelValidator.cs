using FluentValidation;
using Marqa.Service.Services.Exams.Models;

namespace Marqa.Service.Validators.Exams;

public class ExamCreateModelValidator : AbstractValidator<ExamCreateModel>
{
    public ExamCreateModelValidator()
    {
        RuleFor(e => e.CourseId).GreaterThan(0);
        RuleFor(e => e.Title).NotNull();
        RuleFor(e => e.CourseId).GreaterThan(0);
        RuleFor(e => e.ExamSetting).SetValidator(new ExamSettingDataValidator());
    }   
}