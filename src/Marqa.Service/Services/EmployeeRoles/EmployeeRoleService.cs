using Marqa.DataAccess.Repositories;
using Marqa.Domain.Entities;
using Marqa.Service.Exceptions;
using Marqa.Service.Services.EmployeeRoles.Models;
using Microsoft.EntityFrameworkCore;

namespace Marqa.Service.Services.EmployeeRoles;

public class EmployeeRoleService : IEmployeeRoleService
{
    private readonly IRepository<EmployeeRole> employeeRoleRepository;
    private readonly IRepository<Company> companyRepository;
    public EmployeeRoleService()
    {
        employeeRoleRepository = new Repository<EmployeeRole>();
        companyRepository = new Repository<Company>();
    }
    public async Task CreateAsync(EmployeeRoleCreateModel model)
    {
        _ = await companyRepository.SelectAsync(model.CompanyId)
          ?? throw new NotFoundException("Company not found");

        var existrole = await employeeRoleRepository.SelectAllAsQueryable()
            .Where(e => e.CompanyId == model.CompanyId && e.Name == model.Name)
            .FirstOrDefaultAsync();

        if (existrole != null)
            throw new AlreadyExistException("This role already exists");

        await employeeRoleRepository.InsertAsync(new EmployeeRole
        {
            Name = model.Name,
            CompanyId = model.CompanyId,
        });
    }

    public async Task DeleteAsync(int id)
    {
        var existrole = await employeeRoleRepository.SelectAsync(id)
            ?? throw new NotFoundException("Role not found");

        await employeeRoleRepository.DeleteAsync(existrole);
    }

    public async Task<List<EmployeeRoleViewModel>> GetAllAsync(int? companyid)
    {
        var query = employeeRoleRepository.SelectAllAsQueryable();
        if(companyid == null)
            query = query.Where(x => x.CompanyId == companyid);

        return await query
            .Include(x => x.Company)
            .Select(s => new EmployeeRoleViewModel
            {
                Id = s.Id,
                CompanyId = s.CompanyId,
                Name = s.Name,
                Company = new EmployeeRoleViewModel.CompanyInfo
                {
                    Id = s.CompanyId,
                    Name = s.Company.Name
                }
            }).ToListAsync();
    }

    public async Task<EmployeeRoleViewModel> GetAsync(int id)
    {
        var existrole = await employeeRoleRepository.SelectAllAsQueryable()
            .Where(x => x.Id == id)
            .Include(c => c.Company)
            .Select(s => new EmployeeRoleViewModel
            {
                Id = s.Id,
                CompanyId = s.CompanyId,
                Name = s.Name,
                Company = new EmployeeRoleViewModel.CompanyInfo
                {
                    Id = s.CompanyId,
                    Name = s.Company.Name
                }
            }).FirstOrDefaultAsync()
            ?? throw new NotFoundException("Role not found");
        return existrole;
    }

    public async Task UpdateAsync(int id, EmployeeRoleUpdateModel model)
    {
        var existemployeerole = await employeeRoleRepository.SelectAsync(id)
             ?? throw new NotFoundException("Role not found");

        var existrole = await employeeRoleRepository.SelectAllAsQueryable()
            .Where(e => e.CompanyId == model.CompanyId && e.Name == model.Name)
            .FirstOrDefaultAsync();

        if (existrole != null)
            throw new AlreadyExistException("This role already exists");

        existemployeerole.Name = model.Name;
        existemployeerole.CompanyId = model.CompanyId;
        await employeeRoleRepository.UpdateAsync(existemployeerole);
    }
}