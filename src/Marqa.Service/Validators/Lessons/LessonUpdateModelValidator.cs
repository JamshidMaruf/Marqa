using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Marqa.Service.Services.Lessons.Models;

namespace Marqa.Service.Validators.Lessons;
public class LessonUpdateModelValidator : AbstractValidator<LessonUpdateModel>
{
    public LessonUpdateModelValidator()
    {
        RuleFor(x => x.TeacherId).NotNull().GreaterThan(0);
        RuleFor(x => x.Date).NotNull();
        RuleFor(x => x.StartTime).NotNull().LessThan(d => d.EndTime);
        RuleFor(x => x.EndTime).NotNull().GreaterThan(d => d.StartTime);
        RuleFor(x => x.Room).NotNull().NotEmpty();
    }
}
