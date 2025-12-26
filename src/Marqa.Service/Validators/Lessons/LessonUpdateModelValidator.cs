using FluentValidation;
using Marqa.Service.Services.Lessons.Models;

namespace Marqa.Service.Validators.Lessons;
public class LessonUpdateModelValidator : AbstractValidator<LessonUpdateModel>
{
    public LessonUpdateModelValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(x => x.Date).NotNull();
        RuleFor(x => x.EndTime).GreaterThan(x => x.StartTime);
        RuleFor(x => x.Room).NotEmpty();
    }
}
