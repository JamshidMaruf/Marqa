using Marqa.Service.Services.HomeTasks.Models;

namespace Marqa.Service.Services.HomeTasks;
public interface IHomeTaskService
{
    Task CreateHomeTaskAsync(HomeTaskCreateModel model);
    Task UpdateHomeTaskAsync(int id, HomeTaskUpdateModel model);
    Task DeleteHomeTaskAsync(int id);
    Task<List<HomeTaskViewModel>> GetHomeTaskAsync(int id);
}
