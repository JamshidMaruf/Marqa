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
        try
        {
            var existCourse = await unitOfWork.Courses.SelectAsync(c => c.Id == model.CourseId)
               ?? throw new NotFoundException("Course is not found");

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

            existCourse.EnrolledStudentCount++;

            // todo: student payment create shu malumotlaga asoslanib create qilinadi
            unitOfWork.Enrollments.Insert(new Enrollment
            {
                CourseId = model.CourseId,
                StudentId = model.StudentId,
                StudentStatus = StudentStatus.Active,
                PaymentType = model.PaymentType,
                Amount = model.PaymentType == CoursePaymentType.DiscountFree ? 0m : model.Amount,
                EnrolledDate = model.EnrollmentDate
            });

            await unitOfWork.SaveAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            throw new RequestRefusedException("Course capacity was reached by another student.");
        }
    }

    public async Task DeleteAsync(DetachModel model)
    {
        var existStudent = await unitOfWork.Students.CheckExistAsync(s => s.Id == model.StudentId);

        if (!existStudent)
            throw new NotFoundException("Student is not found");

        await EnsureEnrollmentsExist(model.StudentId, model.CourseIds);

        var enrollments = await unitOfWork.Enrollments
            .SelectAllAsQueryable(e => e.StudentId == model.StudentId)
            .ToListAsync();
        foreach (var enrollment in enrollments)
            enrollment.StudentStatus = StudentStatus.Detached;

        await unitOfWork.Enrollments.MarkRangeAsDeletedAsync(enrollments);

        //await unitOfWork.EnrollmentCancellations.Insert(new EnrollmentCancellation
        //{
        //    CancelledAt = DateTime.UtcNow,

        //    EnrollmentId = model.
        //})

        await unitOfWork.SaveAsync();
    }

    public async Task FreezeStudent(FreezeModel model)
    {
        var student = await unitOfWork.Students.CheckExistAsync(s => s.Id == model.StudentId);

        if (!student)
            throw new NotFoundException($"No student was not found with ID {model.StudentId}");

        await EnsureEnrollmentsExist(model.StudentId, model.CourseIds);

        var enrollments = await unitOfWork.Enrollments
            .SelectAllAsQueryable(e => e.StudentId == model.StudentId)
            .ToListAsync();

        var enrollmentFrozens = new List<EnrollmentFrozen>();
        foreach (var enrollment in enrollments)
        {
            enrollment.Status = EnrollmentStatus.Frozen;

            enrollmentFrozens.Add(new EnrollmentFrozen
            {
                EnrollmentId = enrollment.Id,
                StartDate = model.StartDate,
                EndDate = model.IsInDefinite ? null : model.EndDate,
                IsInDefinite = model.IsInDefinite,
                Reason = model.Reason
            });
        }

        await unitOfWork.EnrollmentFrozens.InsertRangeAsync(enrollmentFrozens);
        await unitOfWork.SaveAsync();
    }

    public async Task UnFreezeStudent(UnFreezeModel model)
    {
        var enrollments = await unitOfWork.Enrollments
            .SelectAllAsQueryable(e => e.StudentId == model.StudentId &&
            e.Status == EnrollmentStatus.Frozen,
            includes: "EnrollmentFrozen")
            .ToListAsync();

        foreach (var enrollment in enrollments)
        {
            enrollment.Status = EnrollmentStatus.Active;
        }

        await unitOfWork.EnrollmentFrozens.MarkRangeAsDeletedAsync(enrollments.Select(e => e.EnrollmentFrozen));
        await unitOfWork.SaveAsync();
    }

    private async Task EnsureEnrollmentsExist(int studentId, List<int> groupIds)
    {
        var result = new List<int>();
        result = await unitOfWork.Enrollments.SelectAllAsQueryable(e => e.StudentId == studentId)
            .Select(e => e.CourseId)
            .ToListAsync();

        for (int i = 0; i < groupIds.Count(); i++)
            if (!result.Contains(groupIds[i]))
                throw new ArgumentIsNotValidException($"No goup was found with ID {groupIds[i]}");
    }
}
