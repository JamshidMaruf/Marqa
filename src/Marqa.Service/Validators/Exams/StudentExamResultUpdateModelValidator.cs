using FluentValidation;
using Marqa.Service.Services.Exams.Models;

namespace Marqa.Service.Validators.Exams;
public class StudentExamResultUpdateModelValidator : AbstractValidator<StudentExamResultUpdate>
{
    public StudentExamResultUpdateModelValidator()
    {
        RuleFor(x => x.Score)
            .GreaterThan(0).WithMessage("Score must be greater than zero.")
            .Empty().WithMessage("Score is required.");

        RuleFor(x => x.TeacherFeedback)
            .MaximumLength(500).WithMessage("TeacherFeedback must be at most 500 characters.");
    }
}
