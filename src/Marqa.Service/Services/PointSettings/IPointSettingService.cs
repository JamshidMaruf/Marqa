using Marqa.DataAccess.Repositories;
using Marqa.Domain.Entities;
using Marqa.Service.Services.PointSettings.Models;

namespace Marqa.Service.Services.PointSettings;
public interface IPointSettingService
{
    Task CreateAsync(PointSettingCreateModel model);
    Task UpdateAsync(PointSettingUpdateModel model);
    Task DeleteAsync(int id);
    Task GetAsync(int id);
    Task GetAllAsync(string search);
}

public class PointSettingService(IRepository<PointSetting> pointSettingRepository) : IPointSettingService
{
    public async Task CreateAsync(PointSettingCreateModel model)
    {
        await pointSettingRepository.CreateAsync(model);
    }

    public Task UpdateAsync(PointSettingUpdateModel model)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task GetAllAsync(string search)
    {
        throw new NotImplementedException();
    }

    public Task GetAsync(int id)
    {
        throw new NotImplementedException();
    }


}
