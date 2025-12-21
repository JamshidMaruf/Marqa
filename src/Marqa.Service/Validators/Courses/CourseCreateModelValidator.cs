using FluentValidation;
using Marqa.Service.Services.Courses.Models;

namespace Marqa.Service.Validators.Courses;
public class CourseCreateModelValidator : AbstractValidator<CourseCreateModel>
{
    public CourseCreateModelValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(c => c.CompanyId).GreaterThan(0)
            .WithMessage("Company ID must be greater than 0");
        RuleFor(c => c.CompanyId)
            .MustAsync(async (companyid, cancellationToken) =>
            {
                return await unitOfWork.Companies.CheckExistAsync(ex => ex.Id == companyid);
            }).WithMessage("Company does not exist.");

        RuleForEach(c => c.TeacherIds)
            .MustAsync(async (teacherId, cancellationToken) =>
            {
                return await unitOfWork.Teachers.CheckExistAsync(ex => ex.Id == teacherId);
            })
            .WithMessage("Teacher does not exist.");

        RuleFor(c => c.Name).NotEmpty().MaximumLength(255);
        RuleFor(c => c.MaxStudentCount).GreaterThan(0);
    }
}   