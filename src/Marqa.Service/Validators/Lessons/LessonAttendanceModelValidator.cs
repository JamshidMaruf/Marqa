using FluentValidation;
using Marqa.DataAccess.UnitOfWork;
using Marqa.Service.Services.Lessons.Models;

namespace Marqa.Service.Validators.Lessons;
public class LessonAttendanceModelValidator : AbstractValidator<LessonAttendanceModel>
{
    public LessonAttendanceModelValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(x => x.LessonId).GreaterThan(0);
        RuleFor(x => x.LessonId).Must(x => unitOfWork.LessonAttendances.Exist(l => l.Id == x))
            .WithMessage("Lesson with the given ID does not exist.");
        RuleFor(x => x.StudentId).GreaterThan(0);
        RuleFor(x => x.StudentId).Must(x => unitOfWork.Students.Exist(s => s.Id == x))
            .WithMessage("Student with the given ID does not exist.");
        RuleFor(x => x.Status).NotNull().IsInEnum();
    }
}
