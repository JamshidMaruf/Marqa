using Marqa.Service.Services.EmployeeRoles;
using Marqa.Service.Services.EmployeeRoles.Models;
using Microsoft.AspNetCore.Mvc;

namespace Marqa.WebApi.Controllers;

public class EmployeeRolesController(IEmployeeRoleService employeeRoleService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> PostAsync(EmployeeRoleCreateModel model)
    {
        try
        {
            await employeeRoleService.CreateAsync(model);

            return Created();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] EmployeeRoleUpdateModel model)
    {
        try
        {
            await employeeRoleService.UpdateAsync(id, model);

            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        try
        {
            await employeeRoleService.DeleteAsync(id);

            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetAsync(int id)
    {
        try
        {
            var company = await employeeRoleService.GetAsync(id);

            return Ok(company);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{companyId:int}")]
    public async Task<IActionResult> GetAllAsync(int? companyId)
    {
        try
        {
            var companies = await employeeRoleService.GetAllAsync(companyId);

            return Ok(companies);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}

