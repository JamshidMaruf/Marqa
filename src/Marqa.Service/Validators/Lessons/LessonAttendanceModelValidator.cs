using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Marqa.Service.Services.Lessons.Models;

namespace Marqa.Service.Validators.Lessons;
public class LessonAttendanceModelValidator : AbstractValidator<LessonAttendanceModel>
{
    public LessonAttendanceModelValidator()
    {
        RuleFor(x => x.LessonId).NotNull().GreaterThan(0);
        RuleFor(x => x.StudentId).NotNull().GreaterThan(0);
        RuleFor(x => x.LateTimeInMinutes).GreaterThanOrEqualTo(0).WithErrorCode("Late time cannot be negative.");
        RuleFor(x => x.Status).NotEmpty().NotNull();
    }
}
