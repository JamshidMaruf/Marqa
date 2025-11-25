using FluentValidation;
using Marqa.Service.Services.StudentPointHistories.Models;

namespace Marqa.Service.Validators.StudentPointHistories;
public class StudentPointAddModelValidator : AbstractValidator<StudentPointAddModel>
{
    public StudentPointAddModelValidator()
    {
        RuleFor(p => p.StudentId)
            .GreaterThan(0)
            .WithMessage("Student Id is required");

        RuleFor(p => p.Point).GreaterThan(0);

        RuleFor(p => p.Operation).IsInEnum();
    }
}
