using Marqa.Service.Services.Employees;
using Marqa.Service.Services.Employees.Models;
using Marqa.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace Marqa.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController(IEmployeeService employeeService) : ControllerBase
{
    [HttpPost("create")]
    public async Task<IActionResult> PostAsync(EmployeeCreateModel model)
    {
        int id = await employeeService.CreateAsync(model);

        return Ok(new Response
        {
            Status = 201,
            Message = "success"
        });
    }

    [HttpPut("update/{id:int}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] EmployeeUpdateModel model)
    {
        await employeeService.UpdateAsync(id, model);

        return Ok(new Response
        {
            Status = 200,
            Message = "success"
        });
    }

    [HttpDelete("delete/{id:int}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await employeeService.DeleteAsync(id);

        return Ok(new Response
        {
            Status = 200,
            Message = "success"
        });
    }

    [HttpGet("get/{id:int}")]
    public async Task<IActionResult> GetAsync(int id)
    {
        var result = await employeeService.GetAsync(id);

        return Ok(new Response<EmployeeViewModel>
        {
            Status = 200,
            Message = "success",
            Data = result
        });
    }

    [HttpGet("by{companyId:int}")]
    public async Task<IActionResult> GetAllAsync(int companyId, string search = null)
    {
        var result = await employeeService.GetAllAsync(companyId, search);

        return Ok(new Response<IEnumerable<EmployeeViewModel>>
        {
            Status = 200,
            Message = "success",
            Data = result
        });
    }

    [HttpGet("teachers{id:int}")]
    public async Task<IActionResult> GetTeacherAsync(int id)
    {
        var result = await employeeService.GetTeacherAsync(id);

        return Ok(new Response<TeacherViewModel>
        {
            Status = 200,
            Message = "success",
            Data = result
        });
    }

    [HttpGet("teachers/by{companyId:int}")]
    public async Task<IActionResult> GetAllTeachersAsync(int companyId, string search = null, int? subjectId = null)
    {
        var result = await employeeService.GetAllTeachersAsync(companyId, search, subjectId);

        return Ok(new Response<IEnumerable<TeacherViewModel>>
        {
            Status = 200,
            Message = "success",
            Data = result
        });
    }
}
