using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Marqa.DataAccess.UnitOfWork;
using Marqa.Service.Services.HomeTasks.Models;

namespace Marqa.Service.Validators.HomeTask;
public class StudentHomeTaskCreateModelValidator : AbstractValidator<StudentHomeTaskCreateModel>
{
    public StudentHomeTaskCreateModelValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(x => x.StudentId).GreaterThan(0);
        RuleFor(x => x.StudentId).MustAsync(async (studentId, cancellation) => await unitOfWork.Students.CheckExistAsync(t => t.Id == studentId))
            .WithMessage("Student with the given Id does not exist.");
        RuleFor(x => x.HomeTaskId).GreaterThan(0);
        RuleFor(x => x.HomeTaskId).MustAsync(async (studentHomeTaskId, cancellation) => await unitOfWork.StudentHomeTasks.CheckExistAsync(t => t.Id == studentHomeTaskId))
            .WithMessage("Student home task with the given Id does not exist.");
    }
}
