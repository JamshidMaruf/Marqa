using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Marqa.DataAccess.UnitOfWork;
using Marqa.Service.Services.Exams.Models;

namespace Marqa.Service.Validators.Exams;
public class StudentExamResultCreateModelValidator : AbstractValidator<StudentExamResultCreate>
{
    public StudentExamResultCreateModelValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(x => x.StudentId)
            .GreaterThan(0).WithMessage("StudentId is required.");
        RuleFor(x => x.StudentId).Must(x => unitOfWork.Students.Exist(s => s.Id == x))
            .WithMessage("Student with the given StudentId does not exist.");

        RuleFor(x => x.ExamId)
            .GreaterThan(0).WithMessage("ExamId is required.");
        RuleFor(x => x.ExamId).Must(x => unitOfWork.Exams.Exist(e => e.Id == x))
            .WithMessage("Exam with the given ExamId does not exist.");

        RuleFor(x => x.Score)
            .GreaterThan(0).WithMessage("Score must be greater than zero.")
            .Empty().WithMessage("Score is required.");
    }
}
