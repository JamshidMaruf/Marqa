using FluentValidation;
using Marqa.DataAccess.UnitOfWork;
using Marqa.Domain.Entities;
using Marqa.Service.Exceptions;
using Marqa.Service.Extensions;
using Marqa.Service.Services.EmployeeRoles.Models;
using Microsoft.EntityFrameworkCore;

namespace Marqa.Service.Services.EmployeeRoles;

public class EmployeeRoleService(IUnitOfWork unitOfWork,
    IValidator<EmployeeRoleCreateModel> employeeRoleCreateValidator,
    IValidator<EmployeeRoleUpdateModel> employeeUpdateValdator) : IEmployeeRoleService
{
    public async Task CreateAsync(EmployeeRoleCreateModel model)
    {
        await employeeRoleCreateValidator.EnsureValidatedAsync(model);

        _ = await unitOfWork.Companies.SelectAsync(c => c.Id == model.CompanyId)
          ?? throw new NotFoundException("Company not found");

        var existRole = await unitOfWork.EmployeeRoles.SelectAllAsQueryable(
            e => e.CompanyId == model.CompanyId && e.Name == model.Name)
            .FirstOrDefaultAsync();

        if (existRole != null)
            throw new AlreadyExistException("This role already exists");

        unitOfWork.EmployeeRoles.Insert(new EmployeeRole
        {
            Name = model.Name,
            CanTeach = model.CanTeach,
            CompanyId = model.CompanyId
        });

        await unitOfWork.SaveAsync();
    }

    public async Task UpdateAsync(int id, EmployeeRoleUpdateModel model)
    {
        await employeeUpdateValdator.EnsureValidatedAsync(model);

        var existEmployeeRole = await unitOfWork.EmployeeRoles.SelectAsync(e => e.Id == id)
             ?? throw new NotFoundException("Role not found");

        var existRole = await unitOfWork.EmployeeRoles
            .SelectAllAsQueryable(e => e.CompanyId == model.CompanyId && e.Name == model.Name)
            .FirstOrDefaultAsync();

        if (existRole != null)
            throw new AlreadyExistException("This role already exists");

        existEmployeeRole.Name = model.Name;
        existEmployeeRole.CanTeach = model.CanTeach;
        existEmployeeRole.CompanyId = model.CompanyId;

        unitOfWork.EmployeeRoles.Update(existEmployeeRole);
    }

    public async Task DeleteAsync(int id)
    {
        var existRole = await unitOfWork.EmployeeRoles.SelectAsync(e => e.Id == id)
            ?? throw new NotFoundException("Role not found");

        unitOfWork.EmployeeRoles.MarkAsDeleted(existRole);

        await unitOfWork.SaveAsync();
    }

    public async Task<List<EmployeeRoleViewModel>> GetAllAsync(int? companyId)
    {
        return await unitOfWork.EmployeeRoles
            .SelectAllAsQueryable(x => x.CompanyId == companyId,
            includes: "Company")
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

    public async Task AttachPermissionsAsync(int id, List<int> permissionIds)
    {
        _ = await unitOfWork.EmployeeRoles.SelectAsync(e => e.Id == id)
            ?? throw new NotFoundException("Employee role not found");

        var rolePermissions = await unitOfWork.RolePermissions
            .SelectAllAsQueryable(predicate: rp => rp.RoleId == id)
            .ToListAsync();

        if (rolePermissions.Any())
            unitOfWork.RolePermissions.RemoveRange(rolePermissions);

        await unitOfWork.SaveAsync();

        foreach (var permissionId in permissionIds)
        {
            _ = await unitOfWork.Permissions.SelectAsync(e => e.Id == permissionId)
                ?? throw new NotFoundException("Permission not found");

            unitOfWork.RolePermissions.Insert(new RolePermission { RoleId = id, PermissionId = permissionId });
        }

        await unitOfWork.SaveAsync();
    }

    public async Task<EmployeeRoleViewModel> GetAsync(int id)
    {
        var existRole = await unitOfWork.EmployeeRoles
            .SelectAllAsQueryable(x => x.Id == id,
            includes: "Company")
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
    
    
    public async Task<bool> HasPermissionAsync(string roleName, string permissionName)
    {
        var role = await unitOfWork.EmployeeRoles
            .SelectAsync(r => r.Name == roleName);
        
        var permission = await unitOfWork.Permissions.SelectAsync(p => p.Name == permissionName);
        
        return await unitOfWork.RolePermissions
            .SelectAllAsQueryable(
                predicate: r => 
                    r.RoleId == role.Id &&
                    r.PermissionId == permission.Id)
            .AnyAsync();
    }
}