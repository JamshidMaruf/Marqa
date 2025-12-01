using FluentValidation;
using Marqa.DataAccess.UnitOfWork;

namespace Marqa.Service.Services.StudentPayments.Validators;

public class UpdatePaymentModelValidator : AbstractValidator<UpdatePaymentModel>
{
    public UpdatePaymentModelValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(x => x.PaymentId)
            .GreaterThan(0);
        RuleFor(x => x.PaymentId).Must(paymentId => 
            unitOfWork.StudentPaymentOperations.Exist(p => p.Id == paymentId))
            .WithMessage("Payment does not exist.");

        RuleFor(x => x.StudentId)
            .GreaterThan(0);
        RuleFor(x => x.StudentId).Must(studentId => 
            unitOfWork.Students.Exist(s => s.Id == studentId))
            .WithMessage("Student does not exist.");

        RuleFor(x => x.CourseId)
            .GreaterThan(0);
        RuleFor(x => x.CourseId).Must(courseid => 
            unitOfWork.Courses.Exist(c => c.Id == courseid))
            .WithMessage("Course does not exist.");

        RuleFor(x => x.Amount)
            .GreaterThan(0);

        RuleFor(x => x.CoursePrice)
            .GreaterThan(0);

        RuleFor(x => x.PaymentMethod)
            .IsInEnum().WithMessage("Invalid operation method!");

        RuleFor(x => x.PaymentOperationType)
            .IsInEnum().WithMessage("Invalid operation type!");

        RuleFor(x => x.Description).NotEmpty();

        RuleFor(x => x.PaymentOperationType)
            .Must(type => type == Domain.Enums.PaymentOperationType.Income ||
                         type == Domain.Enums.PaymentOperationType.Expense);
    }
}
