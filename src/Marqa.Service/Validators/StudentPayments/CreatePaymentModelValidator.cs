using FluentValidation;
using Marqa.DataAccess.UnitOfWork;

namespace Marqa.Service.Validators.StudentPaymentOperations;
public class CreatePaymentModelValidator : AbstractValidator<CreatePaymentModel>
{
    public CreatePaymentModelValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(x => x.StudentId)
            .GreaterThan(0);

        //RuleFor(x => x.StudentId).Must(studentId =>
        //    unitOfWork.Students.CheckExist(s => s.Id == studentId))
        //    .WithMessage("Student does not exist.");

        RuleFor(x => x.CourseId)
            .GreaterThan(0);

        //RuleFor(x => x.CourseId).Must(courseid =>
        //    unitOfWork.Courses.CheckExist(c => c.Id == courseid))
        //    .WithMessage("Course does not exist.");

        RuleFor(x => x.Amount)
            .GreaterThan(0);

        RuleFor(x => x.CoursePrice)
            .GreaterThan(0);

        RuleFor(x => x.PaymentMethod)
            .IsInEnum().WithMessage("Invalid operation method!");

        RuleFor(x => x.PaymentOperationType)
            .IsInEnum().WithMessage("Invalid operation type!");

        RuleFor(x => x.PaymentOperationType)
            .Must(type => type == Domain.Enums.PaymentOperationType.Income ||
                         type == Domain.Enums.PaymentOperationType.Expense);
    }
}
