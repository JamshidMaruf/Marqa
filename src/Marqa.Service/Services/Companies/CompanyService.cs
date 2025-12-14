using FluentValidation;
using Marqa.Domain.Entities;
using Marqa.Service.Exceptions;
using Marqa.Service.Extensions;
using Marqa.Service.Services.Companies.Models;
using Microsoft.EntityFrameworkCore;

namespace Marqa.Service.Services.Companies;

public class CompanyService(
    IUnitOfWork unitOfWork,
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
            Director = existCompany.Director,
        };
    }

    public async Task<List<CompanyViewModel>> GetAllAsync()
    {
        return await unitOfWork.Companies
            .SelectAllAsQueryable(t => !t.IsDeleted)
            .Select(c => new CompanyViewModel
            {
                Id = c.Id,
                Name = c.Name,
                Address = c.Address,
                Phone = c.Phone,
                Email = c.Email,
                Director = c.Director,
            }).ToListAsync();
    }
}