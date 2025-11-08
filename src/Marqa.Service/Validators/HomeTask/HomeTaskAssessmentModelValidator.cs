using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Marqa.Service.Services.HomeTasks.Models;

namespace Marqa.Service.Validators.HomeTask;
public class HomeTaskAssessmentModelValidator : AbstractValidator<HomeTaskAssessmentModel>
{
    public HomeTaskAssessmentModelValidator()
    {
        RuleFor(x => x.TeacherId).GreaterThan(0);
        RuleFor(x => x.StudentHomeTaskId).GreaterThan(0);
        RuleFor(x => x.FeedBack).MaximumLength(300);
        RuleFor(x => x.Status).IsInEnum().NotNull();
        RuleFor(x => x.Score).GreaterThan(0);
    }
}
