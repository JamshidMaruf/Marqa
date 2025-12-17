using FluentValidation;
using Marqa.Service.Services.Courses.Models;

namespace Marqa.Service.Validators.Courses;
public class CourseCreateModelValidator : AbstractValidator<CourseCreateModel>
{
    public CourseCreateModelValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(c => c.CompanyId).GreaterThan(0);
        RuleFor(c => c.CompanyId).Must(c => unitOfWork.Companies.CheckExist(ex => ex.Id == c))
            .WithMessage("Company does not exist.");

        RuleFor(c => c.TeacherId).GreaterThan(0);
        RuleFor(c => c.TeacherId)
            .MustAsync(async (teacherId, cancellationToken) =>
            {
                return await unitOfWork.Subjects.CheckExistAsync(ex => ex.Id == teacherId);
            })
            .WithMessage("Teacher does not exist.");


        RuleFor(c => c.SubjectId).GreaterThan(0);
        RuleFor(c => c.SubjectId).Must(c => unitOfWork.Subjects.CheckExist(ex => ex.Id == c))
            .WithMessage("Subject does not exist.");

        RuleFor(c => c.Name).NotEmpty().MaximumLength(255);
        RuleFor(c => c.MaxStudentCount).GreaterThan(0);
    }
}   