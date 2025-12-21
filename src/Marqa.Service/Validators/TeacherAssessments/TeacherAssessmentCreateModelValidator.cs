using FluentValidation;
using Marqa.Service.Services.TeacherAssessments.Models;

namespace Marqa.Service.Validators;

public class TeacherAssessmentCreateModelValidator : AbstractValidator<TeacherAssessmentCreateModel>
{
    public TeacherAssessmentCreateModelValidator()
    {
        RuleFor(x => x.TeacherId)
            .GreaterThan(0)
            .WithMessage("Teacher ID is required");

        RuleFor(x => x.StudentId)
            .GreaterThan(0)
            .WithMessage("Student ID is required");

        RuleFor(x => x.CourseId)
            .GreaterThan(0)
            .WithMessage("Course ID is required");

        RuleFor(x => x.Rating)
            .IsInEnum()
            .WithMessage("Invalid rating value");

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Description not empty");
    }
}
