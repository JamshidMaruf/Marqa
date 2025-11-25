using FluentValidation;
using Marqa.Service.Services.Courses.Models;

namespace Marqa.Service.Validators.Courses;
public class CourseCreateModelValidator : AbstractValidator<CourseCreateModel>
{
    public CourseCreateModelValidator()
    {
        RuleFor(c => c.CompanyId).NotNull();
        RuleFor(c => c.TeacherId).NotNull();
        RuleFor(c => c.SubjectId).NotNull();
        RuleFor(c => c.Name).NotEmpty().MaximumLength(255);
        RuleFor(c => c.LessonCount).NotNull();
        RuleFor(c => c.StartDate).NotNull();
        RuleFor(c => c.StartTime).NotNull();
        RuleFor(c => c.EndTime).NotNull();
        RuleFor(c => c.MaxStudentCount).NotNull();
        RuleFor(c => c.Weekdays).NotNull();
    }
}