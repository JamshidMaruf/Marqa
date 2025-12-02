using Marqa.Service.Services.Enrollments.Models;

namespace Marqa.Service.Services.Enrollments;
public interface IEnrollmentService
{
    Task FreezeStudent(FreezeModel model);
    Task UnFreezeStudent(UnFreezeModel model);
    Task CreateAsync(EnrollmentCreateModel model);
    Task DeleteAsync(DetachModel model);
}
