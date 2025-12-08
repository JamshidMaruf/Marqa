using Marqa.Service.Services.Enrollments.Models;

namespace Marqa.Service.Services.Enrollments;
public interface IEnrollmentService
{
    Task FreezeStudentAsync(FreezeModel model);
    Task UnFreezeStudentAsync(UnFreezeModel model);
    Task CreateAsync(EnrollmentCreateModel model);
    Task DeleteAsync(DetachModel model);
    Task MoveStudentCourseAsync(StudentTransferModel model);
    EnrollmentStatusViewModel GetSpecificEnrollmentStatuses();
}
