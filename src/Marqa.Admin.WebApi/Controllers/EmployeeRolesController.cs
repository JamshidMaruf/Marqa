using Marqa.Service.Services.EmployeeRoles;
using Marqa.Service.Services.EmployeeRoles.Models;
using Marqa.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace Marqa.Admin.WebApi.Controllers;

public class EmployeeRolesController(IEmployeeRoleService employeeRoleService) : BaseController
{
    [HttpPost]
    public async Task<IActionResult> PostAsync(EmployeeRoleCreateModel model)
    {
        await employeeRoleService.CreateAsync(model);

        return Ok(new Response
        {
            StatusCode = 200,
            Message = "success",
        });
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] EmployeeRoleUpdateModel model)
    {
        await employeeRoleService.UpdateAsync(id, model);

        return Ok(new Response
        {
            StatusCode = 200,
            Message = "success",
        });
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await employeeRoleService.DeleteAsync(id);

        return Ok(new Response
        {
            StatusCode = 200,
            Message = "success",
        });
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetAsync(int id)
    {
        var role = await employeeRoleService.GetAsync(id);

        return Ok(new Response<EmployeeRoleViewModel>
        {
            StatusCode = 200,
            Message = "success",
            Data = role
        });
    }

    [HttpGet("company/{companyId}")]
    public async Task<IActionResult> GetAllAsync(int companyId)
    {
        var roles = await employeeRoleService.GetAllAsync(companyId);

        return Ok(new Response<List<EmployeeRoleViewModel>>
        {
            StatusCode = 200,
            Message = "success",
            Data = roles
        });
    }
}
