using Marqa.DataAccess.Repositories;
using Marqa.Domain.Entities;
using Marqa.Service.Exceptions;
using Marqa.Service.Services.Companies.Models;
using Microsoft.EntityFrameworkCore;

namespace Marqa.Service.Services.Companies;

public class CompanyService(IRepository<Company> companyRepository) : ICompanyService
{
    public async Task CreateAsync(CompanyCreateModel model)
    {
        var existCompany = await companyRepository
            .SelectAllAsQueryable()
            .FirstOrDefaultAsync(c => c.Name == model.Name);

        await companyRepository.InsertAsync(new Company
        {
            Name = model.Name,
        });
    }

    public async Task UpdateAsync(int id, CompanyUpdateModel model)
    {
        var existCompany = await companyRepository.SelectAsync(id)
            ?? throw new NotFoundException("Company is not found");

        existCompany.Name = model.Name;

        await companyRepository.UpdateAsync(existCompany);
    }

    public async Task DeleteAsync(int id)
    {
        var existCompany = await companyRepository.SelectAsync(id)
            ?? throw new NotFoundException("Company is not found");

        await companyRepository.DeleteAsync(existCompany);
    }

    public async Task<CompanyViewModel> GetAsync(int id)
    {
        var existCompany = await companyRepository.SelectAsync(id)
            ?? throw new NotFoundException("Company is not found");

        return new CompanyViewModel
        {
            Id = existCompany.Id,
            Name = existCompany.Name,
        };
    }

    public async Task<List<CompanyViewModel>> GetAllAsync()
    {
        return await companyRepository.SelectAllAsQueryable()
            .Where(t => !t.IsDeleted)
            .Select(c => new CompanyViewModel
            {
                Name = c.Name,
            }).ToListAsync();
    }
}