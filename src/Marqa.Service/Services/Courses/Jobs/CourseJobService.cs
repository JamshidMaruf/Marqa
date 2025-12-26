using Marqa.Domain.Entities;
using Marqa.Domain.Enums;
using Marqa.Service.Services.Courses.Models;

namespace Marqa.Service.Services.Courses.Jobs;

public class CourseJobService(IUnitOfWork unitOfWork) : ICourseJobService
{
    public async Task MergeAsync(CourseMergeModel model)
    {
        var fromCourse = await unitOfWork.Courses.SelectAsync(c => c.Id == model.FromCourseId,
            includes: ["Enrollments.Student", "Enrollments"]);

        var toCourse = await unitOfWork.Courses.SelectAsync(c => c.Id == model.ToCourseId,
            includes: "Enrollments");

        foreach (var student in fromCourse.Enrollments.Select(x => x.Student))
        {
            unitOfWork.Enrollments.Insert(new Enrollment
            {
                StudentId = student.Id,
                CourseId = model.ToCourseId,
                PaymentType = CoursePaymentType.DiscountFree,
                Amount = toCourse.Price,
                CoursePrice = toCourse.Price,
                EnrolledDate = model.DateofMerge,
                Status = EnrollmentStatus.Active
            });
        }

        if (toCourse.MaxStudentCount < toCourse.CurrentStudentCount + fromCourse.CurrentStudentCount)
            toCourse.MaxStudentCount += fromCourse.CurrentStudentCount;
        toCourse.CurrentStudentCount += fromCourse.CurrentStudentCount;

        fromCourse.Status = CourseStatus.Merged;

        unitOfWork.Courses.Update(toCourse);
        unitOfWork.Courses.Update(fromCourse);

        await unitOfWork.SaveAsync();
    }
}