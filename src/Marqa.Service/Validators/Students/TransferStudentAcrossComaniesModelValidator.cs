using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Marqa.DataAccess.UnitOfWork;
using Marqa.Service.Services.Courses.Models;
using Marqa.Service.Services.Enrollments.Models;

namespace Marqa.Service.Validators.Students;
public class TransferStudentModelValidator : AbstractValidator<StudentTransferModel>
{
    public TransferStudentModelValidator(IUnitOfWork unitOfWork)
    {
        RuleFor(x => x.StudentId)
            .GreaterThan(0).WithMessage("StudentId must be greater than 0.");
        RuleFor(x => x.StudentId).MustAsync(async (studentId, cancellation) => await unitOfWork.StudentPaymentOperations.CheckExistAsync(p => p.Id == studentId))
            .WithMessage("Student not found.");

        RuleFor(x => x.FromCourseId)
            .GreaterThan(0).WithMessage("FromGroupId must be greater than 0.");
        RuleFor(x => x.FromCourseId).MustAsync(async (courseId, cancellation) => await unitOfWork.Companies.CheckExistAsync(p => p.Id == courseId))
            .WithMessage("FromCourse not found.");

        RuleFor(x => x.ToCourseId)
            .GreaterThan(0).WithMessage("ToGroupId must be greater than 0.")
            .NotEqual(x => x.FromCourseId).WithMessage("ToGroupId must be different from FromGroupId.");
        RuleFor(x => x.ToCourseId).MustAsync(async (courseId, cancellation) =>  await unitOfWork.Companies.CheckExistAsync(p => p.Id == courseId))
            .WithMessage("ToCourse not found.");
        RuleFor(x => x.DateOfTransfer)
            .LessThanOrEqualTo(DateTime.Now).WithMessage("DateOfTransfer cannot be in the future.");
        RuleFor(x => x.Reason)
            .NotEmpty().WithMessage("Reason is required.")
            .MaximumLength(500).WithMessage("Reason cannot exceed 500 characters.");
    }
}
