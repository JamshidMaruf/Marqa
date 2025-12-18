using FluentValidation;
using Marqa.DataAccess.UnitOfWork;
using Marqa.Service.Services.Courses.Models;

namespace Marqa.Service.Validators.Courses;

public class CourseUpdateModelValidator : AbstractValidator<CourseUpdateModel>
{
    public CourseUpdateModelValidator(IUnitOfWork unitOfWork)
    {
        //RuleFor(c => c.TeacherId).GreaterThan(0);

        RuleForEach(c => c.TeacherIds)
            .MustAsync(async (teacherId, cancellationToken) =>
            {
                return await unitOfWork.Teachers.CheckExistAsync(ex => ex.Id == teacherId);
            })
            .WithMessage("Teacher does not exist.");

        RuleFor(c => c.Name).NotEmpty().MaximumLength(255);
        RuleFor(c => c.MaxStudentCount).GreaterThan(0);
        RuleFor(c => c.Weekdays).IsInEnum();
    }
}
