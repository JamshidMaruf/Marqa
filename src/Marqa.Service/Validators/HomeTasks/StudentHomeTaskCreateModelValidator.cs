using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Marqa.Service.Services.HomeTasks.Models;

namespace Marqa.Service.Validators.HomeTasks;
public class StudentHomeTaskCreateModelValidator : AbstractValidator<StudentHomeTaskCreateModel>
{
    public StudentHomeTaskCreateModelValidator()
    {
        RuleFor(x => x.StudentId).NotNull().GreaterThan(0);
        RuleFor(x => x.HomeTaskId).NotNull().GreaterThan(0);
        RuleFor(x => x.Info).NotNull().MaximumLength(2000);
    }
}
