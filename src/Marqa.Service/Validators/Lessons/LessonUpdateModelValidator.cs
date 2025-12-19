using FluentValidation;
using Marqa.DataAccess.UnitOfWork;
using Marqa.Service.Services.Lessons.Models;

namespace Marqa.Service.Validators.Lessons;
public class LessonUpdateModelValidator : AbstractValidator<LessonUpdateModel>
{
    public LessonUpdateModelValidator(IUnitOfWork unitOfWork)
    {
        RuleForEach(x => x.TeacherIds).GreaterThan(0).WithMessage("Teacher id must be greater than zero.");
        RuleForEach(x => x.TeacherIds).MustAsync(async (teacherId, cancellation) => await  unitOfWork.Teachers.CheckExistAsync(t => t.Id == teacherId))
            .WithMessage("Teacher with the given ID does not exist.");
        RuleFor(x => x.Date).NotNull();
        RuleFor(x => x.StartTime).LessThan(x => x.EndTime);
        RuleFor(x => x.EndTime).GreaterThan(x => x.StartTime);
        RuleFor(x => x.Room).NotNull().NotEmpty();
    }
}
