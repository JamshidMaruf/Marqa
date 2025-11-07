using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Marqa.Service.Services.HomeTasks.Models;

namespace Marqa.Service.Validators.HomeTasks;
public class HomeTaskUpdateModelValidator : AbstractValidator<HomeTaskUpdateModel>
{
    public HomeTaskUpdateModelValidator()
    {
        RuleFor(x => x.LessonId).NotNull().GreaterThan(0);
        RuleFor(x => x.Description).MaximumLength(1000);
        RuleFor(x => x.Deadline).NotNull().GreaterThan(DateTime.UtcNow).WithErrorCode("Deadline must be a future date.");
    }
}
