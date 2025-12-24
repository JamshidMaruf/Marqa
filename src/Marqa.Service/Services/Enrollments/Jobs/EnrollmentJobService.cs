using Hangfire;
using Marqa.Domain.Entities;
using Marqa.Domain.Enums;
using Marqa.Service.Exceptions;
using Marqa.Service.Services.Enrollments.Models;
using Microsoft.EntityFrameworkCore;

namespace Marqa.Service.Services.Enrollments.Jobs;

public class EnrollmentJobService(IUnitOfWork unitOfWork) : IEnrollmentJobService
{
    public async Task FreezeAsync(FreezeModel model)
    {
        var student = await unitOfWork.Students.SelectAsync(s => s.Id == model.StudentId)
                      ?? throw new NotFoundException($"No student was not found with ID {model.StudentId}");

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
                () => UnfreezeAsync(new UnFreezeModel
                {
                    StudentId = model.StudentId,
                    CourseIds = model.CourseIds,
                    ActivateDate = model.EndDate.Value
                }),
                model.EndDate.Value);
        }

        await EnsureStudentStatusUptoDateAfterFrozenAsync(model, student);
    }

    public async Task UnfreezeAsync(UnFreezeModel model)
    {
        var enrollments = await unitOfWork.Enrollments
            .SelectAllAsQueryable(predicate: e =>
                    e.StudentId == model.StudentId &&
                    e.Status == EnrollmentStatus.Frozen &&
                    model.CourseIds.Contains(e.CourseId))
            .Include(e => e.EnrollmentFrozens.Where(f => !f.IsUnFrozen))
            .ToListAsync();

        foreach (var enrollment in enrollments)
        {
            enrollment.Status = EnrollmentStatus.Active;
            unitOfWork.Enrollments.Update(enrollment);
        }

        var student = await unitOfWork.Students.SelectAsync(s => s.Id == model.StudentId);

        if (student.Status is not StudentStatus.Active)
            student.Status = StudentStatus.Active;

        unitOfWork.Students.Update(student);

        // add job schedule

        await unitOfWork.EnrollmentFrozens.UpdateRangeAsync(enrollments.Select(e => e.EnrollmentFrozens.First()));
        await unitOfWork.SaveAsync();
    }

    public async Task TransferAsync(StudentTransferModel model)
    {
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

            // 4. Add new course record
            var newStudentCourse = new Enrollment
            {
                Status = model.Status,
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

    public async Task DetachAsync(DetachModel model)
    {
        var existStudent = await unitOfWork.Students.SelectAsync(s => s.Id == model.StudentId)
                           ?? throw new NotFoundException("Student is not found");
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
                CancelledAt = model.DeactivatedDate,
            });
        }

        await unitOfWork.SaveAsync();

        await EnsureStudentStatusUptoDateAfterDeleteAsync(model, existStudent);
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
                 e.Status == EnrollmentStatus.Test ||
                 e.Status == EnrollmentStatus.Frozen));

        if (!haveActiveEnrollments)
        {
            student.Status = StudentStatus.Dropped;
            unitOfWork.Students.Update(student);
            await unitOfWork.SaveAsync();
        }
    }
}