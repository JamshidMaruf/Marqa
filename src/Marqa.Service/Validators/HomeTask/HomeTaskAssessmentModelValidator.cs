using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Marqa.DataAccess.UnitOfWork;
using Marqa.Service.Services.HomeTasks.Models;

namespace Marqa.Service.Validators.HomeTask;
public class HomeTaskAssessmentModelValidator : AbstractValidator<HomeTaskAssessmentModel>
{
    public HomeTaskAssessmentModelValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(x => x.TeacherId).GreaterThan(0);
        RuleFor(x => x.TeacherId).MustAsync(async (teacherId, cancellation) => await unitOfWork.Teachers.CheckExistAsync(t => t.Id == teacherId))
            .WithMessage("Teacher with the given Id does not exist.");
        RuleFor(x => x.StudentHomeTaskId).GreaterThan(0);
        RuleFor(x => x.StudentHomeTaskId).MustAsync(async (studentHomeTaskId, cancellation) => await unitOfWork.StudentHomeTasks.CheckExistAsync(t => t.Id == studentHomeTaskId))
            .WithMessage("Student home task with the given Id does not exist.");
        RuleFor(x => x.Status).IsInEnum();
        RuleFor(x => x.Score).GreaterThan(0);
    }
}
