using FluentValidation;
using Marqa.DataAccess.UnitOfWork;
using Marqa.Service.Services.Courses.Models;

namespace Marqa.Service.Validators.Courses;
public class CourseCreateModelValidator : AbstractValidator<CourseCreateModel>
{
    public CourseCreateModelValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(c => c.CompanyId).GreaterThan(0);
        RuleFor(c => c.CompanyId).Must(c => unitOfWork.Companies.Exist(ex => ex.Id == c))
            .WithMessage("Company does not exist.");

        RuleFor(c => c.TeacherId).GreaterThan(0);
        RuleFor(c => c.TeacherId).Must(c => unitOfWork.Employees.Exist(ex => ex.Id == c))
            .WithMessage("Teacher does not exist.");

        RuleFor(c => c.SubjectId).GreaterThan(0);
        RuleFor(c => c.SubjectId).Must(c => unitOfWork.Subjects.Exist(ex => ex.Id == c))
            .WithMessage("Subject does not exist.");

        RuleFor(c => c.Name).NotEmpty().MaximumLength(255);
        RuleFor(c => c.LessonCount).GreaterThan(0);
        RuleFor(x => x.EndTime)
            .GreaterThan(x => x.StartTime)
            .WithMessage("End time must be greater than start time.");
        RuleFor(c => c.MaxStudentCount).GreaterThan(0);
        RuleFor(c => c.Weekdays).IsInEnum();
    }
}