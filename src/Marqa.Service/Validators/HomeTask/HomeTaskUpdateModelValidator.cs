using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Marqa.DataAccess.UnitOfWork;
using Marqa.Service.Services.HomeTasks.Models;

namespace Marqa.Service.Validators.HomeTask;
public class HomeTaskUpdateModelValidator : AbstractValidator<HomeTaskUpdateModel>
{
    public HomeTaskUpdateModelValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(x => x.LessonId).GreaterThan(0);
        RuleFor(x => x.LessonId).Must(x => unitOfWork.Lessons.Exist(l => l.Id == x))
            .WithMessage("Lesson with the given Id does not exist.");
        RuleFor(x => x.Deadline).GreaterThan(DateTime.UtcNow);
    }
}
