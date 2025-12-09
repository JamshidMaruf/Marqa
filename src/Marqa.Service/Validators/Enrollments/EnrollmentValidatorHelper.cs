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
        var course = await unitOfWork.Courses.SelectAsync(c => c.Id == courseId);

        if (course == null)
            return false;


        var existingEnrollment = await unitOfWork.Enrollments.CheckExistAsync(
            e => e.StudentId == studentId && e.CourseId == courseId);

        return !existingEnrollment;

    }
}

