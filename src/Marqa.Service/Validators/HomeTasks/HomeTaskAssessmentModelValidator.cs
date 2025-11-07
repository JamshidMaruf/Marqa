using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Marqa.Service.Services.HomeTasks.Models;

namespace Marqa.Service.Validators.HomeTasks;
public class HomeTaskAssessmentModelValidator : AbstractValidator<HomeTaskAssessmentModel>
{
    public HomeTaskAssessmentModelValidator()
    {
        RuleFor(x => x.StudentHomeTaskId).NotNull().GreaterThan(0);
        RuleFor(x => x.TeacherId).NotNull().GreaterThan(0);
        RuleFor(x => x.Score).NotNull().InclusiveBetween(0, 100);
        RuleFor(x => x.FeedBack).MaximumLength(1000);
        RuleFor(x => x.Status).IsInEnum().NotNull().WithErrorCode("Invalid status value.");
    }
}
