using Marqa.DataAccess.Repositories;
using Marqa.DataAccess.UnitOfWork;
using Marqa.Domain.Entities;
using Marqa.Service.Exceptions;
using Marqa.Service.Services.Banners.Models;
using Microsoft.EntityFrameworkCore;

public class BannerService(IUnitOfWork unitOfWork) : IBannerService
{
    public async Task CreateAsync(BannerCreateModel model)
    {
        // Validation: StartDate EndDate dan kichik bo'lishi kerak
        if (model.StartDate >= model.EndDate)
            throw new ArgumentException("Start date must be earlier than end date");

        await unitOfWork.Banners.InsertAsync(new Banner
        {
            Title = model.Title,
            Description = model.Description,
            ImageUrl = model.ImageUrl,
            LinkUrl = model.LinkUrl,
            DisplayOrder = model.DisplayOrder,
            IsActive = model.IsActive,
            StartDate = model.StartDate,
            EndDate = model.EndDate
        });

        await unitOfWork.SaveAsync();
    }

    public async Task UpdateAsync(int id, BannerUpdateModel model)
    {
        var existBanner = await unitOfWork.Banners
            .SelectAllAsQueryable()
            .Where(b => !b.IsDeleted)
            .FirstOrDefaultAsync(b => b.Id == id)
            ?? throw new NotFoundException("Banner was not found");

        // Validation
        if (model.StartDate >= model.EndDate)
            throw new ArgumentException("Start date must be earlier than end date");

        existBanner.Title = model.Title;
        existBanner.Description = model.Description;
        existBanner.ImageUrl = model.ImageUrl;
        existBanner.LinkUrl = model.LinkUrl;
        existBanner.DisplayOrder = model.DisplayOrder;
        existBanner.IsActive = model.IsActive;
        existBanner.StartDate = model.StartDate;
        existBanner.EndDate = model.EndDate;

        await unitOfWork.Banners.UpdateAsync(existBanner);
        await unitOfWork.SaveAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var existBanner = await unitOfWork.Banners
            .SelectAllAsQueryable()
            .Where(b => !b.IsDeleted)
            .FirstOrDefaultAsync(b => b.Id == id)
            ?? throw new NotFoundException("Banner was not found");

        await unitOfWork.Banners.DeleteAsync(existBanner);
        await unitOfWork.SaveAsync();
    }

    public async Task<BannerViewModel> GetAsync(int id)
    {
        var existBanner = await unitOfWork.Banners
            .SelectAllAsQueryable()
            .Where(b => b.Id == id && !b.IsDeleted)
            .Select(b => new BannerViewModel
            {
                Id = b.Id,
                Title = b.Title,
                Description = b.Description,
                ImageUrl = b.ImageUrl,
                LinkUrl = b.LinkUrl,
                DisplayOrder = b.DisplayOrder,
                IsActive = b.IsActive,
                StartDate = b.StartDate,
                EndDate = b.EndDate,
                CreatedAt = b.CreatedAt
            })
            .FirstOrDefaultAsync()
            ?? throw new NotFoundException("Banner was not found");

        return existBanner;
    }

    public async Task<List<BannerViewModel>> GetAllAsync()
    {
        return await unitOfWork.Banners
            .SelectAllAsQueryable()
            .Where(b => !b.IsDeleted)
            .OrderBy(b => b.DisplayOrder)
            .Select(b => new BannerViewModel
            {
                Id = b.Id,
                Title = b.Title,
                Description = b.Description,
                ImageUrl = b.ImageUrl,
                LinkUrl = b.LinkUrl,
                DisplayOrder = b.DisplayOrder,
                IsActive = b.IsActive,
                StartDate = b.StartDate,
                EndDate = b.EndDate,
                CreatedAt = b.CreatedAt
            })
            .ToListAsync();
    }

    public async Task<List<BannerViewModel>> GetActiveAsync()
    {
        var currentDate = DateTime.UtcNow;

        return await unitOfWork.Banners
            .SelectAllAsQueryable()
            .Where(b => !b.IsDeleted
                && b.IsActive
                && b.StartDate <= currentDate
                && b.EndDate >= currentDate)
            .OrderBy(b => b.DisplayOrder)
            .Select(b => new BannerViewModel
            {
                Id = b.Id,
                Title = b.Title,
                Description = b.Description,
                ImageUrl = b.ImageUrl,
                LinkUrl = b.LinkUrl,
                DisplayOrder = b.DisplayOrder,
                IsActive = b.IsActive,
                StartDate = b.StartDate,
                EndDate = b.EndDate,
                CreatedAt = b.CreatedAt
            })
            .ToListAsync();
    }

    public async Task ToggleActiveAsync(int id)
    {
        var existBanner = await unitOfWork.Banners
            .SelectAllAsQueryable()
            .Where(b => !b.IsDeleted)
            .FirstOrDefaultAsync(b => b.Id == id)
            ?? throw new NotFoundException("Banner was not found");

        existBanner.IsActive = !existBanner.IsActive;

        await unitOfWork.Banners.UpdateAsync(existBanner);
        await unitOfWork.SaveAsync();
    }

    public async Task<List<BannerViewModel>> GetByCompanyIdAsync(int companyId)
    {
        var banners = await unitOfWork.Banners 
            .SelectAllAsQueryable()
            .Where(b => b.CompanyId == companyId && b.IsActive)
            .OrderBy(b => b.CreatedAt)
            .ToListAsync();

        return banners.Select(b => new BannerViewModel
        {
            Id = b.Id,
            Title = b.Title,
            Description = b.Description,
            ImageUrl = b.ImageUrl,
            LinkUrl = b.LinkUrl,
            DisplayOrder = b.DisplayOrder,
            IsActive = b.IsActive,
            StartDate = b.StartDate,
            EndDate = b.EndDate,
            CreatedAt = b.CreatedAt
        }).ToList();
    }
}
