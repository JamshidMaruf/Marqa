using FluentValidation;
using Marqa.DataAccess.UnitOfWork;
using Marqa.Domain.Entities;
using Marqa.Service.Exceptions;
using Marqa.Service.Services.Banners.Models;
using Marqa.Service.Services.Files;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Marqa.Service.Services.Banners;

public class BannerService(
    IUnitOfWork unitOfWork,
    IFileService fileService,
    IValidator<BannerCreateModel> createValidator,
    IValidator<BannerUpdateModel> updateValidator) : IBannerService
{
    public async Task CreateAsync(BannerCreateModel model)
    {
        var validationResult = await createValidator.ValidateAsync(model);
        if (!validationResult.IsValid)
            throw new ArgumentIsNotValidException(validationResult.Errors.FirstOrDefault().ErrorMessage);

        unitOfWork.Banners.Insert(new Banner
        {
            Title = model.Title,
            Description = model.Description,
            LinkUrl = model.LinkUrl,
            DisplayOrder = model.DisplayOrder,
            IsActive = model.IsActive,
            StartDate = model.StartDate,
            EndDate = model.EndDate,
            CompanyId = model.CompanyId
        });

        await unitOfWork.SaveAsync();
    }

    public async Task UploadBannerImageAsync(int bannerId, IFormFile file)
    {
        var existBanner = await unitOfWork.Banners
            .SelectAllAsQueryable()
            .Where(b => !b.IsDeleted)
            .FirstOrDefaultAsync(b => b.Id == bannerId)
            ?? throw new NotFoundException($"Banner was not found with this ID = {bannerId}");

        string[] imageExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".webp", ".svg", ".tiff", ".ico" };

        var fileExtension = Path.GetExtension(file.FileName).ToLower();

        if (!imageExtensions.Contains(fileExtension))
            throw new ArgumentException("Only image files are allowed for banner");

        var result = await fileService.UploadAsync(file, "Files/Images");

        existBanner.FileName = result.FileName;
        existBanner.FilePath = result.FilePath;
        existBanner.FileExtension = fileExtension;

        unitOfWork.Banners.Update(existBanner);
        await unitOfWork.SaveAsync();
    }

    public async Task UpdateAsync(int id, BannerUpdateModel model)
    {
        var validationResult = await updateValidator.ValidateAsync(model);
        if (!validationResult.IsValid)
            throw new ArgumentIsNotValidException(validationResult.Errors.FirstOrDefault().ErrorMessage);

        var existBanner = await unitOfWork.Banners
            .SelectAllAsQueryable()
            .Where(b => !b.IsDeleted)
            .FirstOrDefaultAsync(b => b.Id == id)
            ?? throw new NotFoundException("Banner was not found");

        existBanner.Title = model.Title;
        existBanner.Description = model.Description;
        existBanner.LinkUrl = model.LinkUrl;
        existBanner.DisplayOrder = model.DisplayOrder;
        existBanner.IsActive = model.IsActive;
        existBanner.StartDate = model.StartDate;
        existBanner.EndDate = model.EndDate;

        unitOfWork.Banners.Update(existBanner);
        await unitOfWork.SaveAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var existBanner = await unitOfWork.Banners
            .SelectAllAsQueryable()
            .Where(b => !b.IsDeleted)
            .FirstOrDefaultAsync(b => b.Id == id)
            ?? throw new NotFoundException("Banner was not found");

        unitOfWork.Banners.MarkAsDeleted(existBanner);
        await unitOfWork.SaveAsync();
    }

    public async Task<MainPageBannerViewModel> GetAsync(int id)
    {
        var existBanner = await unitOfWork.Banners
            .SelectAllAsQueryable()
            .Where(b => b.Id == id && !b.IsDeleted)
            .Select(b => new MainPageBannerViewModel
            {
                Id = b.Id,
                ImageUrl = b.FilePath,
                LinkUrl = b.LinkUrl,
                DisplayOrder = b.DisplayOrder,
            })
            .FirstOrDefaultAsync()
            ?? throw new NotFoundException("Banner was not found");

        return existBanner;
    }

    public async Task<List<MainPageBannerViewModel>> GetAllAsync()
    {
        return await unitOfWork.Banners
            .SelectAllAsQueryable()
            .Where(b => !b.IsDeleted)
            .OrderBy(b => b.DisplayOrder)
            .Select(b => new MainPageBannerViewModel
            {
                Id = b.Id,
                ImageUrl = b.FilePath,
                LinkUrl = b.LinkUrl,
                DisplayOrder = b.DisplayOrder,
            })
            .ToListAsync();
    }

    public async Task<List<MainPageBannerViewModel>> GetActiveAsync()
    {
        var currentDate = DateTime.UtcNow;

        return await unitOfWork.Banners
            .SelectAllAsQueryable()
            .Where(b => !b.IsDeleted
                && b.IsActive
                && b.StartDate <= currentDate
                && b.EndDate >= currentDate)
            .OrderBy(b => b.DisplayOrder)
            .Select(b => new MainPageBannerViewModel
            {
                Id = b.Id,
                ImageUrl = b.FilePath,
                LinkUrl = b.LinkUrl,
                DisplayOrder = b.DisplayOrder,
            })
            .ToListAsync();
    }

    public async Task ToggleActiveAsync(int id)
    {
        var existBanner = await unitOfWork.Banners
         .SelectAsync(b => b.Id == id && !b.IsDeleted)
         ?? throw new NotFoundException("Banner was not found");

        existBanner.IsActive = !existBanner.IsActive;
        await unitOfWork.SaveAsync();
    }

    public async Task<List<MainPageBannerViewModel>> GetByCompanyIdAsync(int companyId)
    {
        return await unitOfWork.Banners
            .SelectAllAsQueryable()
            .Where(b => b.CompanyId == companyId && b.IsActive && !b.IsDeleted)
            .OrderBy(b => b.CreatedAt)
            .Select(b => new MainPageBannerViewModel
            {
                Id = b.Id,
                ImageUrl = b.FilePath,
                LinkUrl = b.LinkUrl,
                DisplayOrder = b.DisplayOrder,
            })
            .ToListAsync();
    }
}