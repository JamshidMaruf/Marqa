using Marqa.Service.Services.HomeTask.Models;

namespace Marqa.Service.Services.HomeTask;
public interface IHomeTaskService
{
    Task CreateHomeTaskAsync(HomeTaskCreateModel model);
    Task UpdateHomeTaskAsync(int id, HomeTaskUpdateModel model);
    Task DeleteHomeTaskAsync(int id);
    Task<List<HomeTaskViewModel>> GetHomeTaskAsync(int id);
}
