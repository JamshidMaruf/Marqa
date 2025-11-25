using FluentValidation;
using Marqa.Service.Services.Lessons.Models;

namespace Marqa.Service.Validators.Lessons;
public class LessonViewModelValidator : AbstractValidator<LessonViewModel>
{
    public LessonViewModelValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.Number).GreaterThan(0).NotNull();
        RuleFor(x => x.Name).NotNull().MaximumLength(255);
        RuleFor(x => x.Date).NotNull();
        RuleFor(x => x.HomeTaskStatus).IsInEnum().NotNull();
    }
}
