using FluentValidation;
using Marqa.DataAccess.UnitOfWork;
using Marqa.Service.Services.Lessons.Models;

namespace Marqa.Service.Validators.Lessons;
public class LessonAttendanceModelValidator : AbstractValidator<LessonAttendanceModel>
{
    public LessonAttendanceModelValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(x => x.LessonId).MustAsync(async (lessonId, cancellation) => await unitOfWork.Lessons.CheckExistAsync(l => l.Id == lessonId))
            .WithMessage("Lesson with the given ID does not exist.");
    }
}
