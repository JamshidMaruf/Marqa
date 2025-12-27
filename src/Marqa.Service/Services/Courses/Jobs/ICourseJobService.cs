using Marqa.Service.Services.Courses.Models;

namespace Marqa.Service.Services.Courses.Jobs;

public interface ICourseJobService : IScopedService
{
    Task MergeAsync(CourseMergeModel model);
}