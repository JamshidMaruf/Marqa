﻿using Marqa.Service.Services;
using Marqa.Service.Services.Banners.Models;
public interface IBannerService : IScopedService
{
    Task CreateAsync(BannerCreateModel model);
    Task UpdateAsync(int id, BannerUpdateModel model);
    Task DeleteAsync(int id);
    Task<MainPageBannerViewModel> GetAsync(int id);
    Task<List<MainPageBannerViewModel>> GetAllAsync();
    Task<List<MainPageBannerViewModel>> GetActiveAsync();
    Task ToggleActiveAsync(int id);
    Task<List<MainPageBannerViewModel>> GetByCompanyIdAsync(int companyId);
}

