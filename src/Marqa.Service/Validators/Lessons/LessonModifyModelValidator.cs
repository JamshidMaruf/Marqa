using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Marqa.Service.Services.Lessons.Models;

namespace Marqa.Service.Validators.Lessons;
public class LessonModifyModelValidator : AbstractValidator<LessonModifyModel>
{
    public LessonModifyModelValidator()
    {
        RuleFor(x => x.Id).NotNull().GreaterThan(0);
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        RuleFor(x => x.HomeTaskStatus).IsInEnum().NotNull().WithErrorCode("Invalid home task status value.");
    }
}
