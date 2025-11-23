using Marqa.Service.Services.Employees;
using Marqa.Service.Services.Employees.Models;
using Marqa.Service.Services.Teachers.Models;
using Marqa.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace Marqa.Admin.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController(IEmployeeService employeeService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> PostAsync(EmployeeCreateModel model)
    {
        await employeeService.CreateAsync(model);

        return Ok(new Response
        {
            StatusCode = 201,
            Message = "success"
        });
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] EmployeeUpdateModel model)
    {
        await employeeService.UpdateAsync(id, model);

        return Ok(new Response
        {
            StatusCode = 200,
            Message = "success"
        });
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await employeeService.DeleteAsync(id);

        return Ok(new Response
        {
            StatusCode = 200,
            Message = "success"
        });
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetAsync(int id)
    {
        var result = await employeeService.GetAsync(id);

        return Ok(new Response<EmployeeViewModel>
        {
            StatusCode = 200,
            Message = "success",
            Data = result
        });
    }

    [HttpGet("{companyId}")]
    public async Task<IActionResult> GetAllAsync(int companyId, [FromQuery] string? search)
    {
        var result = await employeeService.GetAllAsync(companyId, search);

        return Ok(new Response<IEnumerable<EmployeeViewModel>>
        {
            StatusCode = 200,
            Message = "success",
            Data = result
        });
    }
}
