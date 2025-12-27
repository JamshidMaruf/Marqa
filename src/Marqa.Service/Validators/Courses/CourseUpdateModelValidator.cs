using FluentValidation;
using Marqa.Service.Services.Courses.Models;

namespace Marqa.Service.Validators.Courses;

public class CourseUpdateModelValidator : AbstractValidator<CourseUpdateModel>
{
    public CourseUpdateModelValidator(IUnitOfWork unitOfWork)
    {
        RuleForEach(c => c.TeacherIds).GreaterThan(0).WithMessage("Teacher IDs must be greater than 0");
        RuleForEach(c => c.TeacherIds)
            .MustAsync(async (teacherId, cancellationToken) =>
            {
                return await unitOfWork.Teachers.CheckExistAsync(ex => ex.Id == teacherId);
            })
            .WithMessage("Teacher does not exist.");

        RuleFor(c => c.Name).NotEmpty().MaximumLength(255);
        RuleFor(c => c.MaxStudentCount).GreaterThan(0);
        RuleFor(c => c.StartDate).Must(BeValidDate)
                .WithMessage("StartDate not in the weekdays you have chosen!");
    }

    public bool BeValidDate(CourseUpdateModel model, DateOnly startDate)
    {
        if (!model.Weekdays.Select(w => w.DayOfWeek).Contains(startDate.DayOfWeek))
            return false;

        return true;
    }
}
