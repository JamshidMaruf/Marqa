using FluentValidation;
using Marqa.Service.Services.Enrollments.Models;

namespace Marqa.Service.Validators.Students;
public class TransferStudentModelValidator : AbstractValidator<StudentTransferModel>
{
    public TransferStudentModelValidator(IUnitOfWork unitOfWork)
    {   
        RuleFor(x => x.StudentId).MustAsync(async (studentId, cancellation) => await unitOfWork.Students.CheckExistAsync(p => p.Id == studentId))
            .WithMessage("Student not found.");
        
        RuleFor(x => x.FromCourseId).MustAsync(async (courseId, cancellation) => await unitOfWork.Courses.CheckExistAsync(p => p.Id == courseId))
            .WithMessage("FromCourse not found.");

        
        RuleFor(x => x.ToCourseId)
            .NotEqual(x => x.FromCourseId).WithMessage("ToGroupId must be different from FromGroupId.");
        
        RuleFor(x => x.ToCourseId).MustAsync(async (courseId, cancellation) =>  await unitOfWork.Courses.CheckExistAsync(p => p.Id == courseId))
            .WithMessage("ToCourse not found.");
       
        RuleFor(x => x.DateOfTransfer)
            .LessThanOrEqualTo(DateTime.Now).WithMessage("DateOfTransfer cannot be in the future.");
    }
}
