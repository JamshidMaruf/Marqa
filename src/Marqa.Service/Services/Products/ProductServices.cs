using Marqa.DataAccess.Repositories;
using Marqa.Domain.Entities;
using Marqa.Service.Exceptions;
using Marqa.Service.Services.Products.Models;
using Marqa.Service.Services.Products;
using Microsoft.EntityFrameworkCore;
using Marqa.DataAccess.UnitOfWork;
using FluentValidation;
using Marqa.Service.Services.EmployeeRoles.Models;
using Marqa.Service.Services.EmployeeRoles;

namespace Marqa.Service.Servcies.Products;

public class ProductService(IUnitOfWork unitOfWork,
    IValidator<ProductCreateModel> productCreateValidator,
    IValidator<ProductUpdateModel> productUpdateValidator) : IProductService
{
    public async Task CreateAsync(ProductCreateModel model)
    {

        var validatorResult = await productCreateValidator.ValidateAsync(model);
        if (!validatorResult.IsValid)
            throw new ArgumentIsNotValidException(validatorResult.Errors?.FirstOrDefault()?.ErrorMessage);

        var companyExists = await unitOfWork.Companies.SelectAsync(c => c.Id == model.CompanyId)
            ?? throw new InvalidOperationException($"Company with ID {model.CompanyId} does not exist");

        unitOfWork.Products
            .Insert(new Product
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                CompanyId = model.CompanyId
            });

        await unitOfWork.SaveAsync();
    }

    public async Task UpdateAsync(int id, ProductUpdateModel model)
    {
        var validatorResult = await productUpdateValidator.ValidateAsync(model);

        if (!validatorResult.IsValid)
            throw new ArgumentIsNotValidException(validatorResult.Errors?.FirstOrDefault()?.ErrorMessage);

        var product = await unitOfWork.Products.SelectAsync(c => c.Id == id)
            ?? throw new NotFoundException("This product is not found!");

        product.Name = model.Name;
        product.Description = model.Description;
        product.Price = model.Price;

        unitOfWork.Products.Update(product);

        await unitOfWork.SaveAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var result = await unitOfWork.Products.SelectAsync(p => p.Id == id)
            ?? throw new NotFoundException("This product is not found!");

         unitOfWork.Products.MarkAsDeleted(result);

        await unitOfWork.SaveAsync();
    }

    public async Task<ProductViewModel> GetAsync(int id)
    {
        var product = await unitOfWork.Products.SelectAsync(p => p.Id == id)
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

