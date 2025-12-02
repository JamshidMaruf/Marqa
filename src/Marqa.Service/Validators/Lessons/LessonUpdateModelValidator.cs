using FluentValidation;
using Marqa.DataAccess.UnitOfWork;
using Marqa.Service.Services.Lessons.Models;

namespace Marqa.Service.Validators.Lessons;
public class LessonUpdateModelValidator : AbstractValidator<LessonUpdateModel>
{
    public LessonUpdateModelValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(x => x.TeacherId).GreaterThan(0);
        RuleFor(x => x.TeacherId).Must(x => unitOfWork.Employees.CheckExist(t => t.Id == x))
            .WithMessage("Teacher with the given ID does not exist.");
        RuleFor(x => x.Date).NotNull();
        RuleFor(x => x.StartTime).LessThan(x => x.EndTime);
        RuleFor(x => x.EndTime).GreaterThan(x => x.StartTime);
        RuleFor(x => x.Room).NotNull().NotEmpty();
    }
}
