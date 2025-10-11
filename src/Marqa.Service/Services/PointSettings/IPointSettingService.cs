using Marqa.Service.Services.PointSettings.Models;


namespace Marqa.Service.Services.PointSettings;
public interface IPointSettingService
{
    Task CreateAsync(PointSettingCreateModel model);
    Task UpdateAsync(int id, PointSettingUpdateModel model);
    Task DeleteAsync(int id);
    Task<PointSettingViewModel> GetAsync(int id);
    Task<IEnumerable<PointSettingViewModel>> GetAllAsync(string search = null);
    Task ToggleAsync(int id);
    //Task SetOrRemovePointAsync(int studentId);
    Task<string> GenerateToken(TokenModel model);
    Task<TokenModel> DecodeToken(string token);
}
