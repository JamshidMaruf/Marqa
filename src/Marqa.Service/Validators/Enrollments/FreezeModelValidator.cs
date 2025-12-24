using FluentValidation;
using Marqa.Service.Services.Enrollments.Models;

namespace Marqa.Service.Validators.Enrollments;

public class FreezeModelValidator : AbstractValidator<FreezeModel>
{
    public FreezeModelValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(model => model.StudentId).GreaterThan(0).WithMessage("StudentId must be greater than 0");
        RuleFor(model => model.StudentId)
            .MustAsync(async (studentid, cancellation) =>
            {
                return await unitOfWork.Students.CheckExistAsync(s => s.Id == studentid);
            }).WithMessage("Student  not found");
        
        RuleForEach(model => model.CourseIds).GreaterThan(0).WithMessage("CourseIds must be greater than 0");
        RuleForEach(model => model.CourseIds)
            .MustAsync(async (courseid, cancellation) =>
            {
                return await unitOfWork.Courses.CheckExistAsync(c => c.Id == courseid);
            }).WithMessage("Course  not found");
    }
}