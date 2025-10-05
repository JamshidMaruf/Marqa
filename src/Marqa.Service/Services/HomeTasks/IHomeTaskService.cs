using Marqa.Service.Services.HomeTasks.Models;

namespace Marqa.Service.Services.HomeTasks;

public interface IHomeTaskService
{
    Task CreateAsync(HomeTaskCreateModel model);
    Task UpdateAsync(int id, HomeTaskUpdateModel model);
    Task DeleteAsync(int id);
    Task<List<HomeTaskViewModel>> GetAsync(int lessonId);
}
