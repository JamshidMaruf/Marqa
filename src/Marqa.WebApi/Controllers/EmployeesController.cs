using Marqa.Service.Services.Employees;
using Marqa.Service.Services.Employees.Models;
using Microsoft.AspNetCore.Mvc;

namespace Marqa.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController(IEmployeeService employeeService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> PostAsync(EmployeeCreateModel model)
    {
        try
        {
            await employeeService.CreateAsync(model);

            return Created();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] EmployeeUpdateModel model)
    {
        try
        {
            await employeeService.UpdateAsync(id, model);

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
            await employeeService.DeleteAsync(id);

            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("Emloyee/{id:int}")]
    public async Task<IActionResult> GetAsync(int id)
    {
        try
        {
            var result = await employeeService.GetAsync(id);

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("AllEmployees/{companyId:int}")]
    public async Task<IActionResult> GetAllAsync(int companyId, string search = default)
    {
        try
        {
            var result = await employeeService.GetAllAsync(companyId, search);

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("Teacher/{id:int}")]
    public async Task<IActionResult> GetTeacherAsync(int id)
    {
        try
        {
            var result = await employeeService.GetAsync(id);

            return Ok(result);
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("AllTeachers/{companyId:int}")]
    public async Task<IActionResult> GetAllTeachersAsync(int companyId, string search=default, int subjectId=default)
    {
        try
        {
            var result = await employeeService.GetAllTeachersAsync(companyId,search,subjectId);

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
