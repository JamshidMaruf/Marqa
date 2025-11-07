using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Marqa.Service.Services.Exams.Models;

namespace Marqa.Service.Validators.Exams;
public class StudentExamResultCreateModelValidator : AbstractValidator<StudentExamResultCreate>
{
    public StudentExamResultCreateModelValidator()
    {
        RuleFor(x => x.StudentId)
            .GreaterThan(0).WithMessage("StudentId is required.");

        RuleFor(x => x.ExamId)
            .GreaterThan(0).WithMessage("ExamId is required.");

        RuleFor(x => x.Score)
            .GreaterThan(0).WithMessage("Score must be greater than zero.")
            .Empty().WithMessage("Score is required.");

        RuleFor(x => x.TeacherFeedback)
            .MaximumLength(500).WithMessage("TeacherFeedback must be at most 500 characters.");
    }
}
