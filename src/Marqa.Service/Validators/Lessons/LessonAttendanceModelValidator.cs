using FluentValidation;
using Marqa.Service.Services.Lessons.Models;

namespace Marqa.Service.Validators.Lessons;
public class LessonAttendanceModelValidator : AbstractValidator<LessonAttendanceModel>
{
    public LessonAttendanceModelValidator()
    {
        RuleFor(x => x.LessonId).GreaterThan(0);
        RuleFor(x => x.StudentId).GreaterThan(0);
        RuleFor(x => x.Status).NotNull().IsInEnum();
    }
}
