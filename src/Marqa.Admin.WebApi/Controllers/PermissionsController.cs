using Marqa.Service.Services.EmployeeRoles;
using Marqa.Service.Services.Permissions.Models;
using Marqa.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Marqa.Admin.WebApi.Controllers;

[AllowAnonymous]
public class PermissionsController(IPermissionService permissionService, IEmployeeRoleService employeeRoleService) : BaseController
{
    [HttpPost]
    public async Task<IActionResult> PostAsync(PermissionCreateModel model)
    {
        await permissionService.CreateAsync(model);

        return Ok(new Response
        {
            StatusCode = 201,
            Message = "success",
        });
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] PermissionUpdateModel model)
    {
        await permissionService.UpdateAsync(id, model);

        return Ok(new Response
        {
            StatusCode = 200,
            Message = "success"
        });
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await permissionService.DeleteAsync(id);

        return Ok(new Response
        {
            StatusCode = 200,
            Message = "success"
        });
    }
    
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
    public async Task<IActionResult> GetAllAsync()
    {
        var permissions = await permissionService.GetAllAsync();

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