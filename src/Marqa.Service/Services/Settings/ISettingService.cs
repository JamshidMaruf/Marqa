using Marqa.Domain.Entities;
using Marqa.Service.Services.Settings.Models;
using Marqa.Shared.Models;

namespace Marqa.Service.Services.Settings;

public interface ISettingService : IScopedService
{
    Task CreateAsync(SettingCreateModel model);
    Task DeleteAsync(string key);
    Task<SettingViewModel> GetAsync(string key);
    Task<Dictionary<string, string>> GetByCategoryAsync(string category);
    Task<List<Setting>> GetAllAsync(PaginationParams @params, string search = null);
    Task<int> GetSettingsCountAsync();
}