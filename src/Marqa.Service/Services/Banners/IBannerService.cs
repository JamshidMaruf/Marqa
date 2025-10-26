using Marqa.Service.Services.Banners.Models;
public interface IBannerService
{
    Task CreateAsync(BannerCreateModel model);
    Task UpdateAsync(int id, BannerUpdateModel model);
    Task DeleteAsync(int id);
    Task<BannerViewModel> GetAsync(int id);
    Task<List<BannerViewModel>> GetAllAsync();
    Task<List<BannerViewModel>> GetActiveAsync();
    Task ToggleActiveAsync(int id);
    Task<List<BannerViewModel>> GetByCompanyIdAsync(int companyId);
}

