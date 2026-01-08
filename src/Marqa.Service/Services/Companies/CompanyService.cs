using FluentValidation;
using Marqa.Domain.Entities;
using Marqa.Service.Exceptions;
using Marqa.Service.Extensions;
using Marqa.Service.Services.Companies.Models;
using Marqa.Shared.Models;
using Marqa.Shared.Services;
using Microsoft.EntityFrameworkCore;

namespace Marqa.Service.Services.Companies;

public class CompanyService(
    IUnitOfWork unitOfWork,
    IPaginationService paginationService,
    IValidator<CompanyCreateModel> createModelValidator,
    IValidator<CompanyUpdateModel> updateModelValidator) : ICompanyService
{
    public async Task CreateAsync(CompanyCreateModel model)
    {
        await createModelValidator.EnsureValidatedAsync(model);

        unitOfWork.Companies.Insert(new Company
        {
            Name = model.Name,
            Address = model.Address,
            Phone = model.Phone,
            Email = model.Email,
            Director = model.Director
        });

        await unitOfWork.SaveAsync();
    }

    public async Task UpdateAsync(int id, CompanyUpdateModel model)
    {
        await updateModelValidator.EnsureValidatedAsync(model);

        var existCompany = await unitOfWork.Companies.SelectAsync(c => c.Id == id)
            ?? throw new NotFoundException("Company is not found");

        existCompany.Name = model.Name;
        existCompany.Address = model.Address;
        existCompany.Phone = model.Phone;
        existCompany.Email = model.Email;
        existCompany.Director = model.Director;

        unitOfWork.Companies.Update(existCompany);

        await unitOfWork.SaveAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var existCompany = await unitOfWork.Companies.SelectAsync(c => c.Id == id)
            ?? throw new NotFoundException("Company is not found");

        unitOfWork.Companies.MarkAsDeleted(existCompany);

        await unitOfWork.SaveAsync();
    }

    public async Task<CompanyViewModel> GetAsync(int id)
    {
        var existCompany = await unitOfWork.Companies.SelectAsync(c => c.Id == id)
            ?? throw new NotFoundException("Company is not found");

        return new CompanyViewModel
        {
            Id = existCompany.Id,
            Name = existCompany.Name,
            Address = existCompany.Address,
            Phone = existCompany.Phone,
            Email = existCompany.Email,
            Director = existCompany.Director
        };
    }

    public async Task<CompanyUpdateFormModel> GetForUpdateAsync(int id)
    {
        var existCompany = await unitOfWork.Companies.SelectAsync(c => c.Id == id)
            ?? throw new NotFoundException("Company is not found");

        return new CompanyUpdateFormModel
        {
            Id = existCompany.Id,
            Name = existCompany.Name,
            Address = existCompany.Address,
            Phone = existCompany.Phone,
            Email = existCompany.Email,
            Director = existCompany.Director
        };
    }

    public async Task<List<CompanyViewModel>> GetAllAsync(PaginationParams @params, string search = null)
    {
        var query = unitOfWork.Companies.SelectAllAsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            search = search.Trim();

            query = query.Where(c =>
                c.Name.Contains(search) ||
                c.Address.Contains(search) ||
                c.Phone.Contains(search) ||
                c.Email.Contains(search) ||
                c.Director.Contains(search));
        }

        query = paginationService.Paginate(query, @params);

        return await query
            .Select(c => new CompanyViewModel
            {
                Id = c.Id,
                Name = c.Name,
                Address = c.Address,
                Phone = c.Phone,
                Email = c.Email,
                Director = c.Director,
            })
            .ToListAsync();
    }

    public async Task<int> GetCompaniesCountAsync()
    {
        return await unitOfWork.Companies.SelectAllAsQueryable().CountAsync();  
    }
}
