using FluentValidation;
using Marqa.DataAccess.UnitOfWork;
using Marqa.Service.Services.StudentPointHistories.Models;

namespace Marqa.Service.Validators.StudentPointHistories;
public class StudentPointAddModelValidator : AbstractValidator<StudentPointAddModel>
{
    public StudentPointAddModelValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(p => p.StudentId)
            .GreaterThan(0)
            .WithMessage("Student Id is required");
        RuleFor(p => p.StudentId).Must(p => unitOfWork.Students.CheckExist(s => s.Id == p))
            .WithMessage("Student not found");

        RuleFor(p => p.Point).GreaterThan(0);

        RuleFor(p => p.Operation).IsInEnum();
    }
}
