using Marqa.DataAccess.Repositories;
using Marqa.Domain.Entities;
using Marqa.Service.Exceptions;
using Marqa.Service.Services.Products.Models;
using Marqa.Service.Services.Products;
using Microsoft.EntityFrameworkCore;
using Marqa.DataAccess.UnitOfWork;

namespace Marqa.Service.Servcies.Products;

public class ProductService(IUnitOfWork unitOfWork) : IProductService
{
    public async Task CreateAsync(ProductCreateModel model)
    {
        var companyExists = await unitOfWork.Companies.SelectAsync(model.CompanyId)
            ?? throw new InvalidOperationException($"Company with ID {model.CompanyId} does not exist");

        var product = await unitOfWork.Products
            .InsertAsync(new Product
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                CompanyId = model.CompanyId
            });
    }

    public async Task UpdateAsync(int id, ProductUpdateModel model)
    {
        var product = await unitOfWork.Products.SelectAsync(id)
            ?? throw new NotFoundException("This product is not found!");

        product.Name = model.Name;
        product.Description = model.Description;
        product.Price = model.Price;

        await unitOfWork.Products.UpdateAsync(product);
    }

    public async Task DeleteAsync(int id)
    {
        var result = await unitOfWork.Products.SelectAsync(id)
            ?? throw new NotFoundException("This product is not found!");

         await unitOfWork.Products.DeleteAsync(result);
    }

    public async Task<ProductViewModel> GetAsync(int id)
    {
        var product = await unitOfWork.Products.SelectAsync(id)
            ?? throw new NotFoundException("This product is not found!");

        return new ProductViewModel
        {
            Id = product.Id,
            CompanyId = product.CompanyId,
            Name = product.Name,
            Price = product.Price,
        };
    }

    public async Task<List<ProductViewModel>> GetAllAsync(int companyId, string search = null)
    {
        var products = unitOfWork.Products.SelectAllAsQueryable()
            .Where(p => !p.IsDeleted && p.CompanyId == companyId)
            ?? throw new NotFoundException("This product is not found!");

        if (string.IsNullOrEmpty(search))
            products = products.Where(p => p.Name.ToLower() == search.ToLower());

        return await products.Select(p => new ProductViewModel
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            Price = p.Price,
            CompanyId = p.CompanyId,
        })
            .ToListAsync();
    }

}

