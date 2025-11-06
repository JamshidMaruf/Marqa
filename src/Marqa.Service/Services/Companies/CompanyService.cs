using FluentValidation;
using Marqa.DataAccess.Repositories;
using Marqa.DataAccess.UnitOfWork;
using Marqa.Domain.Entities;
using Marqa.Service.Exceptions;
using Marqa.Service.Services.Companies.Models;
using Marqa.Service.Validators.Companies;
using Microsoft.EntityFrameworkCore;

namespace Marqa.Service.Services.Companies;

public class CompanyService(
    IUnitOfWork unitOfWork,
    IValidator<CompanyCreateModel> createModelValidator,
    IValidator<CompanyUpdateModel> updateModelValidator) : ICompanyService
{
    public async Task CreateAsync(CompanyCreateModel model)
    {
        var validatorResult = await createModelValidator.ValidateAsync(model);

        if (!validatorResult.IsValid)
            throw new ArgumentIsNotValidException(validatorResult.Errors.FirstOrDefault().ErrorMessage);

        unitOfWork.Companies.Insert(new Company
        {
            Name = model.Name,
        });

        await unitOfWork.SaveAsync();
    }

    public async Task UpdateAsync(int id, CompanyUpdateModel model)
    {
        var validatorResult = await updateModelValidator.ValidateAsync(model);

        if (!validatorResult.IsValid)
            throw new ArgumentIsNotValidException(validatorResult.Errors.FirstOrDefault().ErrorMessage);

        var existCompany = await unitOfWork.Companies.SelectAsync(c => c.Id == id)
            ?? throw new NotFoundException("Company is not found");

        existCompany.Name = model.Name;

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
        };
    }

    public async Task<List<CompanyViewModel>> GetAllAsync()
    {
        return await unitOfWork.Companies.SelectAllAsQueryable()
            .Where(t => !t.IsDeleted)
            .Select(c => new CompanyViewModel
            {
                Id = c.Id,
                Name = c.Name,
            }).ToListAsync();
    }
}