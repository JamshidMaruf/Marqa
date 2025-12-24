using Marqa.Service.Services.Enrollments.Models;

namespace Marqa.Service.Services.Enrollments.Jobs;
public interface IEnrollmentJobService : IScopedService
{
    Task FreezeAsync(FreezeModel model);
    Task UnfreezeAsync(UnFreezeModel model);
    Task TransferAsync(StudentTransferModel model);
    Task DetachAsync(DetachModel model);
}
