using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Marqa.Service.Services.HomeTasks.Models;

namespace Marqa.Service.Validators.HomeTask;
public class HomeTaskUpdateModelValidator : AbstractValidator<HomeTaskUpdateModel>
{
    public HomeTaskUpdateModelValidator()
    {
        RuleFor(x => x.LessonId).GreaterThan(0);
        RuleFor(x => x.Deadline).GreaterThan(DateTime.UtcNow);
    }
}
