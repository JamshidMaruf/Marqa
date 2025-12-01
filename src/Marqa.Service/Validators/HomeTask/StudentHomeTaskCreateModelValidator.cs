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
        RuleFor(x => x.StudentId).Must(x => unitOfWork.Students.Exist(u => u.Id == x))
            .WithMessage("Student with the given Id does not exist.");
        RuleFor(x => x.HomeTaskId).GreaterThan(0);
        RuleFor(x => x.HomeTaskId).Must(x => unitOfWork.HomeTasks.Exist(ht => ht.Id == x))
            .WithMessage("Home task with the given Id does not exist.");
    }
}
