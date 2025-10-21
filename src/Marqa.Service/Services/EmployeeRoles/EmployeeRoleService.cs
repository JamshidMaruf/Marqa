using Marqa.DataAccess.UnitOfWork;
using Marqa.Domain.Entities;
using Marqa.Service.Exceptions;
using Marqa.Service.Services.EmployeeRoles.Models;
using Microsoft.EntityFrameworkCore;

namespace Marqa.Service.Services.EmployeeRoles;

public class EmployeeRoleService(IUnitOfWork unitOfWork) : IEmployeeRoleService
{
    public async Task CreateAsync(EmployeeRoleCreateModel model)
    {
        _ = await unitOfWork.Companies.SelectAsync(c => c.Id == model.CompanyId)
          ?? throw new NotFoundException("Company not found");

        var existRole = await unitOfWork.EmployeeRoles.SelectAllAsQueryable()
            .Where(e => e.CompanyId == model.CompanyId && e.Name == model.Name)
            .FirstOrDefaultAsync();

        if (existRole != null)
            throw new AlreadyExistException("This role already exists");

        await unitOfWork.EmployeeRoles.InsertAsync(new EmployeeRole
        {
            Name = model.Name,
            CompanyId = model.CompanyId,
        });
    }

    public async Task DeleteAsync(int id)
    {
        var existRole = await unitOfWork.EmployeeRoles.SelectAsync(e => e.Id == id)
            ?? throw new NotFoundException("Role not found");

        await unitOfWork.EmployeeRoles.DeleteAsync(existRole);
    }

    public async Task<List<EmployeeRoleViewModel>> GetAllAsync(int? companyId)
    {
        var query = unitOfWork.EmployeeRoles.SelectAllAsQueryable();
        if(companyId != null)
            query = query.Where(x => x.CompanyId == companyId);

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
        var existRole = await unitOfWork.EmployeeRoles.SelectAllAsQueryable()
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
        return existRole;
    }

    public async Task UpdateAsync(int id, EmployeeRoleUpdateModel model)
    {
        var existEmployeeRole = await unitOfWork.EmployeeRoles.SelectAsync(e => e.Id == id)
             ?? throw new NotFoundException("Role not found");

        var existRole = await unitOfWork.EmployeeRoles.SelectAllAsQueryable()
            .Where(e => e.CompanyId == model.CompanyId && e.Name == model.Name)
            .FirstOrDefaultAsync();

        if (existRole != null)
            throw new AlreadyExistException("This role already exists");

        existEmployeeRole.Name = model.Name;
        existEmployeeRole.CompanyId = model.CompanyId;
        await unitOfWork.EmployeeRoles.UpdateAsync(existEmployeeRole);
    }
}