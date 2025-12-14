using Marqa.DataAccess.UnitOfWork;
using Marqa.Domain.Enums;
using Marqa.Service.Services.Enrollments.Models;

public static class EnrollmentValidatorHelper
{
    public static bool ValidateAmount(EnrollmentCreateModel model, decimal amount)
    {
        if (model.PaymentType == CoursePaymentType.DiscountInPercentage)
            return amount >= 0 && amount <= 100;

        if (model.PaymentType == CoursePaymentType.Fixed)
            return amount >= 0;

        return true; 
    }

    public static async Task<bool> ValidateCourseCapacityAsync(IUnitOfWork unitOfWork, int courseId, int studentId)
    {
        var course = await unitOfWork.Courses.SelectAsync(
            c => c.Id == courseId,
            includes: "Enrollments");

        if (course == null)
            return false;

        var existingEnrollment = await unitOfWork.Enrollments.CheckExistAsync(
            e => e.StudentId == studentId &&
                 e.CourseId == courseId &&
                 !e.IsDeleted &&
                 (e.Status == EnrollmentStatus.Active || e.Status == EnrollmentStatus.Test));

        if (existingEnrollment)
            return false;

        var activeEnrollmentsCount = course.Enrollments?
            .Count(e => !e.IsDeleted &&
                       (e.Status == EnrollmentStatus.Active || e.Status == EnrollmentStatus.Test)) ?? 0;

        return activeEnrollmentsCount < course.MaxStudentCount;
    }
}

