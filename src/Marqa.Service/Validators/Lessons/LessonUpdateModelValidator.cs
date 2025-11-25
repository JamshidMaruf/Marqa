using FluentValidation;
using Marqa.Service.Services.Lessons.Models;

namespace Marqa.Service.Validators.Lessons;
public class LessonUpdateModelValidator : AbstractValidator<LessonUpdateModel>
{
    public LessonUpdateModelValidator()
    {
        RuleFor(x => x.TeacherId).GreaterThan(0);
        RuleFor(x => x.Date).NotNull();
        RuleFor(x => x.StartTime).LessThan(x => x.EndTime);
        RuleFor(x => x.EndTime).GreaterThan(x => x.StartTime);
        RuleFor(x => x.Room).NotNull().NotEmpty();
    }
}
