using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Marqa.Service.Services.HomeTasks.Models;

namespace Marqa.Service.Validators.HomeTasks;
public class HomeTaskCreateModelValidator : AbstractValidator<HomeTaskCreateModel>
{
    public HomeTaskCreateModelValidator()
    {
        RuleFor(x => x.Description).MaximumLength(1000).WithErrorCode("Description cannot exceed 1000 characters.");
        RuleFor(x => x.Deadline).GreaterThan(DateTime.Now).WithErrorCode("Deadline must be a future date.");
        RuleFor(x => x.LessonId).NotNull().GreaterThan(0);
        RuleFor(x => x.Files).NotNull().NotEmpty().WithErrorCode("At least one file must be attached.");
    }
}
