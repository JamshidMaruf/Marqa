﻿using Marqa.Domain.Entities;
using Marqa.Service.Services.EmployeeRoles.Models;

namespace Marqa.Service.Services.EmployeeRoles;
public interface IEmployeeRoleService : IScopedService
{
    Task CreateAsync(EmployeeRoleCreateModel model);
    Task UpdateAsync(int id, EmployeeRoleUpdateModel model);
    Task DeleteAsync(int id);
    Task<EmployeeRoleViewModel> GetAsync(int id);
    Task<List<EmployeeRoleViewModel>> GetAllAsync(int companyId);
    Task AttachPermissionsAsync(int id, List<int> permissionIds);
    Task<bool> HasPermissionAsync(string roleName, string permissionName);
}
