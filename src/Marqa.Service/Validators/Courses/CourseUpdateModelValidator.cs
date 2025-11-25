using FluentValidation;
using Marqa.Service.Services.Courses.Models;

namespace Marqa.Service.Validators.Courses;

public class CourseUpdateModelValidator : AbstractValidator<CourseUpdateModel>
{
    public CourseUpdateModelValidator()
    {
        RuleFor(c => c.TeacherId).GreaterThan(0);
        RuleFor(c => c.Name).NotEmpty().MaximumLength(255);
        RuleFor(c => c.LessonCount).GreaterThan(0);
        RuleFor(x => x.EndTime)
            .GreaterThan(x => x.StartTime)
            .WithMessage("End time must be greater than start time.");
        RuleFor(c => c.MaxStudentCount).GreaterThan(0);
        RuleFor(c => c.Weekdays).IsInEnum();
    }
}
