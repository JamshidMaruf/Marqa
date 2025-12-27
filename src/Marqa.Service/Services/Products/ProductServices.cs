using FluentValidation;
using Marqa.Domain.Entities;
using Marqa.Service.Exceptions;
using Marqa.Service.Extensions;
using Marqa.Service.Services.Products.Models;
using Marqa.Shared.Models;
using Marqa.Shared.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Marqa.Service.Services.Products;

public class ProductService(
    IUnitOfWork unitOfWork,
    IPaginationService paginationService,
    IFileService fileService,
    IValidator<ProductCreateModel> productCreateValidator,
    IValidator<ProductUpdateModel> productUpdateValidator) : IProductService
{
    public async Task CreateAsync(ProductCreateModel model)
    {
        await productCreateValidator.EnsureValidatedAsync(model);

        unitOfWork.Products
            .Insert(new Product
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                CompanyId = model.CompanyId,
                IsDisplayed = model.IsDisplayed
            });

        await unitOfWork.SaveAsync();
    }

    public async Task UpdateAsync(int id, ProductUpdateModel model)
    {
        await productUpdateValidator.EnsureValidatedAsync(model);

        var product = await unitOfWork.Products.SelectAsync(c => c.Id == id)
            ?? throw new NotFoundException("This product is not found!");

        product.Name = model.Name;
        product.Description = model.Description;
        product.Price = model.Price;
        product.IsDisplayed = model.IsDisplayed;

        unitOfWork.Products.Update(product);

        await unitOfWork.SaveAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var result = await unitOfWork.Products.SelectAsync(p => p.Id == id,
            includes: "Asset")
            ?? throw new NotFoundException("This product is not found!");

        unitOfWork.Products.MarkAsDeleted(result);

        await unitOfWork.ProductImages.MarkRangeAsDeletedAsync(result.Images);

        await unitOfWork.SaveAsync();
    }

    public async Task<ProductViewModel> GetAsync(int id)
    {
        var product = await unitOfWork.Products.SelectAsync(p => p.Id == id,
            includes: "Company")
            ?? throw new NotFoundException("This product is not found!");

        var totalPurchases = await unitOfWork.OrderItems
            .SelectAllAsQueryable(item => item.ProductId == product.Id)
            .CountAsync();

        return new ProductViewModel
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            TotalPurchases = totalPurchases,
            Company = new ProductViewModel.CompanyInfo
            {
                Id = product.Company.Id,
                Name = product.Company.Name
            },
            Description = product.Description,
            IsDisplayed = product.IsDisplayed
        };
    }

    public async Task<ProductUpdateFormModel> GetForUpdateAsync(int id)
    {
        var product = await unitOfWork.Products.SelectAsync(p => p.Id == id,
            includes: "Company")
            ?? throw new NotFoundException("This product is not found!");

        return new ProductUpdateFormModel
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            Description = product.Description,
            IsDisplayed = product.IsDisplayed,
            Company = new ProductUpdateFormModel.CompanyInfo
            {
                Id = product.Company.Id,
                Name = product.Company.Name
            }
        };
    }

    public async Task<List<ProductTableModel>> GetAllAsync(
        int companyId,
        PaginationParams @params,
        string search = null)
    {
        var query = unitOfWork.Products
            .SelectAllAsQueryable(p => p.CompanyId == companyId);

        if (!string.IsNullOrWhiteSpace(search))
        {
            var searchText = search.ToLower();

            query = query.Where(p =>
                        p.Name.ToLower().Contains(searchText) ||
                        p.Description.ToLower().Contains(searchText));
        }

        return await paginationService.Paginate(query, @params)
            .Select(p => new ProductTableModel
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                CompanyId = p.CompanyId,
                IsDisplayed = p.IsDisplayed
            })
            .ToListAsync();
    }

    public async Task UploadPictureAsync(int productId, List<IFormFile> files)
    {
        var product = await unitOfWork.Products.SelectAsync(p => p.Id == productId)
            ?? throw new NotFoundException("Product was not found!");

        var uploadedImages = new List<Asset>();

        foreach (var file in files)
        {
            var fileExtension = Path.GetExtension(file.FileName);

            if (!fileService.IsImageExtension(fileExtension))
                throw new ArgumentIsNotValidException("File is not valid. Please send only image file!");

            var imageData = await fileService.UploadAsync(file, "images/products");

            uploadedImages.Add(new Asset
            {
                FileName = imageData.FileName,
                FilePath = imageData.FilePath,
                FileExtension = fileExtension
            });
        }

        var transaction = await unitOfWork.BeginTransactionAsync();
        try
        {
            await unitOfWork.Assets.InsertRangeAsync(uploadedImages);

            await unitOfWork.SaveAsync();

            foreach (var uploadedImage in uploadedImages)
            {
                unitOfWork.ProductImages.Insert(new ProductImage
                {
                    ImageId = uploadedImage.Id,
                    ProductId = product.Id
                });
            }

            await unitOfWork.SaveAsync();

            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }

    }

    public async Task RemoveImageAsync(int productId, int imageId)
    {
        var product = await unitOfWork.Products.SelectAsync(p => p.Id == productId)
           ?? throw new NotFoundException("Product was not found!");

        var image = await unitOfWork.Assets.SelectAsync(a => a.Id == imageId)
            ?? throw new NotFoundException("Image was not found!");

        try
        {
            if (File.Exists(image.FilePath))
            {
                File.Delete(image.FilePath);

                unitOfWork.Assets.Remove(image);
                await unitOfWork.SaveAsync();
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}

