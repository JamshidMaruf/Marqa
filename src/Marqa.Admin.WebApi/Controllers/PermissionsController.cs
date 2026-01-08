using Marqa.Service.Services.EmployeeRoles;
using Marqa.Service.Services.Permissions;
using Marqa.Service.Services.Permissions.Models;
using Marqa.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Marqa.Admin.WebApi.Controllers;

[AllowAnonymous]
public class PermissionsController(IPermissionService permissionService, IEmployeeRoleService employeeRoleService) : BaseController
{    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetAsync(int id)
    {
        var permission = await permissionService.GetAsync(id);

        return Ok(new Response<PermissionViewModel>
        {
            StatusCode = 200,
            Message = "success",
            Data = permission
        });
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync(PaginationParams @params, string? search)
    {
        var permissions = await permissionService.GetAllAsync(@params, search);

        return Ok(new Response<List<PermissionViewModel>>
        {
            StatusCode = 200,
            Message = "success",
            Data = permissions
        });
    }
    
    [HttpPost("roles/{roleId:int}")]
    public async Task<IActionResult> AttachPermissionsAsync(int roleId, [FromBody] List<int> permissionIds)
    {
        await employeeRoleService.AttachPermissionsAsync(roleId, permissionIds);

        return Ok(new Response
        {
            StatusCode = 200,
            Message = "success",
        });
    }
}