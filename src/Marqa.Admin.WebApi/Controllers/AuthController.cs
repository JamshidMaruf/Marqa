using Marqa.Service.Services.Employees;
using Marqa.Service.Services.Employees.Models;
using Marqa.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace Marqa.Admin.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IEmployeeService employeeService) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] EmployeeLoginModel model)
    {
        return Ok(new Response<EmployeeLoginViewModel>
        {
            StatusCode = 200, Message = "success", Data = await employeeService.LoginAsync(model)
        });
    }
}