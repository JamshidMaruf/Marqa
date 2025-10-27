using Marqa.Service.Services.Settings.Models;

namespace Marqa.Service.Services.Settings;

public interface ISettingService
{
    Task CreateAsync(SettingCreateModel model);
    Task DeleteAsync(string key);
    Task<SettingViewModel> GetAsync(string key);
    Task<Dictionary<string, string>> GetByCategoryAsync(string category);
}