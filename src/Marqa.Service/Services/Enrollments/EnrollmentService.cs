using FluentValidation;
using Hangfire;
using Marqa.Domain.Entities;
using Marqa.Domain.Enums;
using Marqa.Service.Exceptions;
using Marqa.Service.Extensions;
using Marqa.Service.Services.Enrollments.Jobs;
using Marqa.Service.Services.Enrollments.Models;
using Marqa.Service.Services.Enums;
using Microsoft.EntityFrameworkCore;

namespace Marqa.Service.Services.Enrollments;

public class EnrollmentService(IUnitOfWork unitOfWork,
    IValidator<EnrollmentCreateModel> enrollmentCreateValidator,
    IValidator<StudentTransferModel> transferValidator,
    IValidator<FreezeModel> freezeValidator,
    IEnumService enumService,
    IEnrollmentJobService enrollmentJobService) : IEnrollmentService
{
    public async Task CreateAsync(EnrollmentCreateModel model)
    {
        var existCourse = await unitOfWork.Courses.SelectAsync(c => c.Id == model.CourseId)
            ?? throw new NotFoundException("Course is not found");

        await enrollmentCreateValidator.EnsureValidatedAsync(model);

        decimal coursePrice = 0m;
        if (model.PaymentType == CoursePaymentType.Fixed)
            coursePrice = model.Amount;
        else if (model.PaymentType == CoursePaymentType.DiscountFree)
            coursePrice = existCourse.Price;
        else if (model.PaymentType == CoursePaymentType.DiscountInPercentage)
            coursePrice = existCourse.Price / 100 * (100 - model.Amount);

        var enrollment = new Enrollment
        {
            CourseId = model.CourseId,
            StudentId = model.StudentId,
            PaymentType = model.PaymentType,
            Amount = model.PaymentType == CoursePaymentType.DiscountFree ? 0m : model.Amount,
            EnrolledDate = model.EnrollmentDate,
            Status = model.Status,
            CoursePrice = coursePrice
        };

        var student = await unitOfWork.Students.SelectAsync(s => s.Id == model.StudentId);

        if (student.Status is not StudentStatus.Active)
            student.Status = StudentStatus.Active;

        unitOfWork.Students.Update(student);

        unitOfWork.Enrollments.Insert(enrollment);

        await unitOfWork.SaveAsync();
    }

    public async Task DetachAsync(DetachModel model)
    {
        await EnsureEnrollmentsExistAsync(model.StudentId, model.CourseIds);
        BackgroundJob.Schedule(() => enrollmentJobService.DetachAsync(model), model.DeactivatedDate);

    }

    public async Task FreezeStudentAsync(FreezeModel model)
    {
        await freezeValidator.ValidateAsync(model);
        await EnsureEnrollmentsExistAsync(model.StudentId, model.CourseIds);
        
        BackgroundJob.Schedule<IEnrollmentJobService>(job => job.FreezeAsync(model),
            model.StartDate.ToUniversalTime());
    }

    public async Task UnFreezeStudentAsync(UnFreezeModel model)
    {
        await EnsureEnrollmentsExistAsync(model.StudentId, model.CourseIds);
        BackgroundJob.Schedule(() => enrollmentJobService.UnfreezeAsync(model),
            model.ActivateDate
        );
    }

    public async Task MoveStudentCourseAsync(StudentTransferModel model)
    {
        await transferValidator.EnsureValidatedAsync(model);
        await enrollmentCreateValidator.ValidateAsync(new EnrollmentCreateModel
        {
            StudentId = model.StudentId,
            CourseId = model.ToCourseId,
            EnrollmentDate = model.DateOfTransfer,
            Amount = model.Amount,
            PaymentType = model.PaymentType,
            Status = model.Status,
        });
        var targetCourse = await unitOfWork.Courses
                               .SelectAsync(c => c.Id == model.ToCourseId && c.Status != CourseStatus.Closed || c.Status != CourseStatus.Completed) 
                           ?? throw new NotFoundException("Target course not found or finished");
        
        if (targetCourse.EndDate <= DateOnly.FromDateTime(model.DateOfTransfer))
        {
            throw new ArgumentIsNotValidException($"Target course will be finished");
        }

        BackgroundJob.Schedule(() => enrollmentJobService.TransferAsync(model),
            model.DateOfTransfer
        );
    }

    public EnrollmentStatusViewModel GetSpecificEnrollmentStatuses()
    {
        var specificEnum = new EnrollmentStatusViewModel();
        specificEnum.Statuses = new List<EnrollmentStatusViewModel.EnrollmentStatusData>
        {
            new EnrollmentStatusViewModel.EnrollmentStatusData{
                Id = (int)EnrollmentStatus.Active,
                Name = enumService.GetEnumDescription(EnrollmentStatus.Active)
            },
            new EnrollmentStatusViewModel.EnrollmentStatusData
            {
                Id = (int)EnrollmentStatus.Test,
                Name = enumService.GetEnumDescription(EnrollmentStatus.Test)
            },
            new EnrollmentStatusViewModel.EnrollmentStatusData
            {
                Id = (int)EnrollmentStatus.Completed,
                Name = enumService.GetEnumDescription(EnrollmentStatus.Completed)
            }
        };

        return specificEnum;
    }

    private async Task EnsureEnrollmentsExistAsync(int studentId, List<int> courseIds)
    {
        var allStudentCourseIds = new HashSet<int>(
            await unitOfWork.Enrollments
            .SelectAllAsQueryable(e => e.StudentId == studentId)
            .Select(e => e.CourseId)
            .ToListAsync());

        var missings = courseIds.Where(id => !allStudentCourseIds.Contains(id)).ToList();

        if (missings.Count > 0)
            throw new ArgumentIsNotValidException($"These groups were not found for this student: {string.Join(", ", missings)}");
    }
}
