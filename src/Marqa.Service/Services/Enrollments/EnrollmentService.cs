using Marqa.DataAccess.UnitOfWork;
using Marqa.Domain.Entities;
using Marqa.Domain.Enums;
using Marqa.Service.Exceptions;
using Marqa.Service.Services.Enrollments.Models;
using Microsoft.EntityFrameworkCore;

namespace Marqa.Service.Services.Enrollments;

public class EnrollmentService(IUnitOfWork unitOfWork) : IEnrollmentService
{
    public async Task CreateAsync(EnrollmentCreateModel model)
    {
        var existCourse = await unitOfWork.Courses.SelectAsync(c => c.Id == model.CourseId)
                          ?? throw new NotFoundException("Course is not found");
// add this check logic to validator
        var existStudent = await unitOfWork.Students.CheckExistAsync(s => s.Id == model.StudentId);

        if (!existStudent)
            throw new NotFoundException("Student is not found");

        if (existCourse.MaxStudentCount == existCourse.EnrolledStudentCount)
            throw new RequestRefusedException("This course has reached its maximum number of students.");

        if (model.PaymentType == CoursePaymentType.DiscountInPercentage)
            if (model.Amount > 100 || model.Amount < 0)
                throw new ArgumentIsNotValidException("Invalid amount");

            else if (model.PaymentType == CoursePaymentType.Fixed)
                if (model.Amount < 0)
                    throw new ArgumentIsNotValidException("Invalid amount");

        if (model.EnrollmentDate > DateTime.UtcNow)
            throw new ArgumentIsNotValidException("Enrollment date cannot be in the future");
//
        existCourse.EnrolledStudentCount++;
        unitOfWork.Courses.Update(existCourse);

        unitOfWork.Enrollments.Insert(new Enrollment
        {
            CourseId = model.CourseId,
            StudentId = model.StudentId,
            PaymentType = model.PaymentType,
            Amount = model.PaymentType == CoursePaymentType.DiscountFree ? 0m : model.Amount,
            EnrolledDate = model.EnrollmentDate
        });

        await unitOfWork.SaveAsync();
    }

    public async Task DeleteAsync(DetachModel model)
    {
        var existStudent = await unitOfWork.Students.SelectAsync(s => s.Id == model.StudentId)
            ?? throw new NotFoundException("Student is not found");
        
        await EnsureEnrollmentsExist(model.StudentId, model.CourseIds);
        
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

    public async Task FreezeStudent(FreezeModel model)
    {
        var student = await unitOfWork.Students.SelectAsync(s => s.Id == model.StudentId)
            ?? throw new NotFoundException($"No student was not found with ID {model.StudentId}");

        await EnsureEnrollmentsExist(model.StudentId, model.CourseIds);

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

        await EnsureStudentStatusUptoDateAfterFrozenAsync(model,student);
    }

    public async Task UnFreezeStudent(UnFreezeModel model)
    {
        var enrollments = await unitOfWork.Enrollments
            .SelectAllAsQueryable(predicate: e => 
                    e.StudentId == model.StudentId &&
                    e.Status == EnrollmentStatus.Frozen &&
                    model.CourseIds.Contains(e.CourseId))
            .ToListAsync();

        foreach (var enrollment in enrollments)
        {
            enrollment.Status = EnrollmentStatus.Active;
        }
        
        // TODO: Add hangfire for activate courses

        await unitOfWork.EnrollmentFrozens.MarkRangeAsDeletedAsync(enrollments.Select(e => e.EnrollmentFrozen));
        await unitOfWork.SaveAsync();
    }

    private async Task EnsureEnrollmentsExist(int studentId, List<int> courseIds)
    {
        var result = new List<int>();
        result = await unitOfWork.Enrollments.SelectAllAsQueryable(e => e.StudentId == studentId)
            .Select(e => e.CourseId)
            .ToListAsync();

        for (int i = 0; i < courseIds.Count(); i++)
            if (!result.Contains(courseIds[i]))
                throw new ArgumentIsNotValidException($"No goup was found with ID {courseIds[i]} for this student");
    }

    private async Task EnsureStudentStatusUptoDateAfterFrozenAsync(FreezeModel model, Student student)
    {
        var haveActiveEnrollments = await unitOfWork.Enrollments
        .SelectAllAsQueryable()
        .AnyAsync(e =>
            e.StudentId == model.StudentId &&
            e.Status == EnrollmentStatus.Active &&
            e.Status == EnrollmentStatus.Test);

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
            e.Status == EnrollmentStatus.Active &&
            e.Status == EnrollmentStatus.Test);

        if (!haveActiveEnrollments)
        {
            if (student.Status != StudentStatus.Active)
            {
                student.Status = StudentStatus.Dropped;
                unitOfWork.Students.Update(student);
                await unitOfWork.SaveAsync();
            }
        }
    }
}
