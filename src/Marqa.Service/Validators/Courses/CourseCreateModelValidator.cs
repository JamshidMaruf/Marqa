using FluentValidation;
using Marqa.Service.Services.Courses.Models;

namespace Marqa.Service.Validators.Courses;
public class CourseCreateModelValidator : AbstractValidator<CourseCreateModel>
{
    public CourseCreateModelValidator()
    {
        RuleFor(c => c.CompanyId).NotNull().GreaterThan(0);
        RuleFor(c => c.TeacherId).NotNull().GreaterThan(0);
        RuleFor(c => c.SubjectId).NotNull().GreaterThan(0);
        RuleFor(c => c.Name).NotEmpty().Length(36);
        RuleFor(c => c.LessonCount).NotNull().GreaterThan(0);
        RuleFor(c => c.StartDate).NotNull();
        RuleFor(c => c.StartTime).NotNull();
        RuleFor(c => c.EndTime).NotNull();
        RuleFor(c => c.MaxStudentCount).NotNull();
        RuleFor(c => c.Weekdays).NotNull();
    }
}