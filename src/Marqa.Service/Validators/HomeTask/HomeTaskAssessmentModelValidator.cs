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
        RuleFor(x => x.TeacherId).Must(x => unitOfWork.Employees.CheckExist(t => t.Id == x))
            .WithMessage("Teacher with the given Id does not exist.");
        RuleFor(x => x.StudentHomeTaskId).GreaterThan(0);
        RuleFor(x => x.StudentHomeTaskId).Must(x => unitOfWork.StudentHomeTasks.CheckExist(sht => sht.Id == x))
            .WithMessage("Student home task with the given Id does not exist.");
        RuleFor(x => x.Status).IsInEnum();
        RuleFor(x => x.Score).GreaterThan(0);
    }
}
