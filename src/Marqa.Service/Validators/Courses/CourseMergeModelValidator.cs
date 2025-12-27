using FluentValidation;
using Marqa.Service.Services.Courses.Models;

namespace Marqa.Service.Validators.Courses;

public class CourseMergeModelValidator : AbstractValidator<CourseMergeModel>
{
    public CourseMergeModelValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(model => model.FromCourseId)
            .MustAsync(async (id, cancellation) =>
            {
                return await unitOfWork.Courses.CheckExistAsync(c => c.Id == id);
            }).WithMessage("FromCourseId does not exist");

        RuleFor(model => model.ToCourseId)
            .MustAsync(async (id, cancellation) =>
            {
                return await unitOfWork.Courses.CheckExistAsync(c => c.Id == id);
            }).WithMessage("ToCourseId does not exist");
    }
}