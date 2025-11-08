using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Marqa.Service.Services.HomeTasks.Models;

namespace Marqa.Service.Validators.HomeTask;
public class HomeTaskCreateModelValidator : AbstractValidator<HomeTaskCreateModel>
{
    public HomeTaskCreateModelValidator()
    {
        RuleFor(x => x.LessonId).GreaterThan(0);
        RuleFor(x => x.Deadline).NotNull().GreaterThan(DateTime.Now);
        RuleFor(x => x.Files).NotEmpty().NotNull();
        RuleFor(x => x.Description).MaximumLength(1000);
    }
}
