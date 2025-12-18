using FluentValidation;
using Marqa.DataAccess.UnitOfWork;
using Marqa.Service.Services.Lessons.Models;

namespace Marqa.Service.Validators.Lessons;
public class LessonAttendanceModelValidator : AbstractValidator<LessonAttendanceModel>
{
    public LessonAttendanceModelValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(x => x.LessonId).GreaterThan(0);
        RuleFor(x => x.LessonId).MustAsync(async (lessonId, cancellation) => await unitOfWork.Lessons.CheckExistAsync(l => l.Id == lessonId))
            .WithMessage("Lesson with the given ID does not exist.");
        RuleFor(x => x.StudentId).GreaterThan(0);
        RuleFor(x => x.StudentId).MustAsync(async  (studentId, cancellation) => await unitOfWork.Students.CheckExistAsync(s => s.Id == studentId))
            .WithMessage("Student with the given ID does not exist.");
        RuleFor(x => x.Status).NotNull().IsInEnum();
    }
}
