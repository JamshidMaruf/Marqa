using FluentValidation;
using Hangfire;
using Marqa.Domain.Entities;
using Marqa.Domain.Enums;
using Marqa.Service.Exceptions;
using Marqa.Service.Extensions;
using Marqa.Service.Services.Enrollments.Models;
using Marqa.Service.Services.Enums;
using Microsoft.EntityFrameworkCore;

namespace Marqa.Service.Services.Enrollments;

public class EnrollmentService(IUnitOfWork unitOfWork,
    IValidator<EnrollmentCreateModel> enrollmentCreateValidator,
    IValidator<StudentTransferModel> transferValidator,
    IEnumService enumService) : IEnrollmentService
{
    public async Task CreateAsync(EnrollmentCreateModel model)
    {
        var existCourse = await unitOfWork.Courses.SelectAsync(c => c.Id == model.CourseId)
            ?? throw new NotFoundException("Course is not found");

        await enrollmentCreateValidator.ValidateAndThrowAsync(model);

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

        unitOfWork.Enrollments.Insert(enrollment);

        await unitOfWork.SaveAsync();
    }

    public async Task DetachAsync(DetachModel model)
    {
        var existStudent = await unitOfWork.Students.SelectAsync(s => s.Id == model.StudentId)
            ?? throw new NotFoundException("Student is not found");

        await EnsureEnrollmentsExistAsync(model.StudentId, model.CourseIds);

        var enrollments = await unitOfWork.Enrollments
            .SelectAllAsQueryable(e =>
                e.StudentId == model.StudentId &&
                model.CourseIds.Contains(e.CourseId))
            .ToListAsync();

        foreach (var enrollment in enrollments)
        {
            enrollment.Status = EnrollmentStatus.Dropped;
            unitOfWork.Enrollments.Update(enrollment);

            unitOfWork.EnrollmentCancellations.Insert(new EnrollmentCancellation
            {
                EnrollmentId = enrollment.Id,
                Reason = model.Reason,
                CancelledAt = DateTime.UtcNow,
            });
        }

        await unitOfWork.SaveAsync();

        await EnsureStudentStatusUptoDateAfterDeleteAsync(model, existStudent);
    }

    public async Task FreezeStudentAsync(FreezeModel model)
    {
        var student = await unitOfWork.Students.SelectAsync(s => s.Id == model.StudentId)
            ?? throw new NotFoundException($"No student was not found with ID {model.StudentId}");

        await EnsureEnrollmentsExistAsync(model.StudentId, model.CourseIds);

        var enrollments = await unitOfWork.Enrollments
            .SelectAllAsQueryable(e =>
                e.StudentId == model.StudentId &&
                model.CourseIds.Contains(e.CourseId))
            .ToListAsync();

        foreach (var enrollment in enrollments)
        {
            enrollment.Status = EnrollmentStatus.Frozen;
            unitOfWork.Enrollments.Update(enrollment);

            unitOfWork.EnrollmentFrozens.Insert(new EnrollmentFrozen
            {
                EnrollmentId = enrollment.Id,
                Reason = model.Reason,
                StartDate = model.StartDate,
                EndDate = model.IsInDefinite ? null : model.EndDate,
                IsInDefinite = model.IsInDefinite,
            });
        }

        await unitOfWork.SaveAsync();

        foreach (var enrollment in enrollments)
        {
            unitOfWork.Enrollments.DetachFromChangeTracker(enrollment);
        }

        await unitOfWork.SaveAsync();

        if (!model.IsInDefinite)
        {
            var unFreezeModel = new UnFreezeModel
            {
                CourseIds = model.CourseIds,
                StudentId = model.StudentId,
                ActivateDate = model.EndDate.Value
            };

            BackgroundJob.Schedule(
                () => UnFreezeStudentAsync(unFreezeModel),
                model.EndDate.Value);
        }

        await EnsureStudentStatusUptoDateAfterFrozenAsync(model, student);
    }

    public async Task UnFreezeStudentAsync(UnFreezeModel model)
    {
        var enrollments = await unitOfWork.Enrollments
            .SelectAllAsQueryable(predicate: e =>
                e.StudentId == model.StudentId &&
                e.Status == EnrollmentStatus.Frozen &&
                model.CourseIds.Contains(e.CourseId),
                includes: "EnrollmentFrozens")
            .ToListAsync();

        foreach (var enrollment in enrollments)
        {
            enrollment.Status = EnrollmentStatus.Active;
            unitOfWork.Enrollments.Update(enrollment);
        }

        // add job schedule

        await unitOfWork.EnrollmentFrozens.MarkRangeAsDeletedAsync(enrollments.Select(e => e.EnrollmentFrozens.Last()));
        await unitOfWork.SaveAsync();
    }

    public async Task MoveStudentCourseAsync(StudentTransferModel model)
    {
        //add hangfire to schedule the transfer time
        await transferValidator.EnsureValidatedAsync(model);

        using var transaction = await unitOfWork.BeginTransactionAsync();

        try
        {
            // 1. Load studentCourse from the source course
            var studentCourse = await unitOfWork.Enrollments
                .SelectAsync(sc => sc.StudentId == model.StudentId
                                   && sc.CourseId == model.FromCourseId)
                ?? throw new NotFoundException("Student is not enrolled in the source course");

            // 2. Load the target course & ensure NOT finished
            var targetCourse = await unitOfWork.Courses
                .SelectAsync(c => c.Id == model.ToCourseId && c.Status != CourseStatus.Closed
                || c.Status != CourseStatus.Completed,
                includes: "Enrollments")
                ?? throw new NotFoundException("Target course not found or finished");

            // 3. Remove from old course
            unitOfWork.Enrollments.MarkAsDeleted(studentCourse);
            await unitOfWork.SaveAsync();

            #region movetovalidator

            if (targetCourse.MaxStudentCount == targetCourse.Enrollments.Count)
                throw new RequestRefusedException("This course has reached its maximum number of students.");

            if (model.PaymentType == CoursePaymentType.DiscountInPercentage)
                if (model.Amount > 100 || model.Amount < 0)
                    throw new ArgumentIsNotValidException("Invalid amount");

                else if (model.PaymentType == CoursePaymentType.Fixed)
                    if (model.Amount < 0)
                        throw new ArgumentIsNotValidException("Invalid amount");

            if (model.DateOfTransfer > DateTime.UtcNow)
                throw new ArgumentIsNotValidException("Enrollment date cannot be in the future");
            #endregion

            // 4. Add new course record
            var newStudentCourse = new Enrollment
            {
                StudentId = model.StudentId,
                CourseId = model.ToCourseId,
                EnrolledDate = model.DateOfTransfer,
                Amount = model.PaymentType == CoursePaymentType.DiscountFree ? 0m : model.Amount,
                PaymentType = model.PaymentType,
            };

            unitOfWork.Enrollments.Insert(newStudentCourse);
            await unitOfWork.SaveAsync();

            var newTransfer = new EnrollmentTransfer
            {
                FromEnrollmentId = studentCourse.Id,
                ToEnrollmentId = newStudentCourse.Id,
                Reason = model.Reason,
                TransferTime = model.DateOfTransfer
            };

            unitOfWork.EnrollmentTransfers.Insert(newTransfer);
            await unitOfWork.SaveAsync();

            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
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

    private async Task EnsureStudentStatusUptoDateAfterFrozenAsync(FreezeModel model, Student student)
    {
        var haveActiveEnrollments = await unitOfWork.Enrollments
        .SelectAllAsQueryable()
        .AnyAsync(e =>
            e.StudentId == model.StudentId &&
            (e.Status == EnrollmentStatus.Active ||
            e.Status == EnrollmentStatus.Test));

        if (!haveActiveEnrollments)
        {
            student.Status = StudentStatus.InActive;
            unitOfWork.Students.Update(student);
            await unitOfWork.SaveAsync();
        }
    }

    private async Task EnsureStudentStatusUptoDateAfterDeleteAsync(DetachModel model, Student student)
    {
        var haveActiveEnrollments = await unitOfWork.Enrollments
        .SelectAllAsQueryable()
        .AnyAsync(e =>
            e.StudentId == model.StudentId &&
            (e.Status == EnrollmentStatus.Active ||
            e.Status == EnrollmentStatus.Test));

        if (!haveActiveEnrollments)
        {
            student.Status = StudentStatus.Dropped;
            unitOfWork.Students.Update(student);
            await unitOfWork.SaveAsync();
        }
    }
}
