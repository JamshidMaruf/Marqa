﻿using Marqa.Service.Services.Employees.Models;
using Marqa.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace Marqa.Teacher.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    [HttpPost("login")]
    public IActionResult Login([FromBody] EmployeeLoginModel model)
    {
        // TODO: Implement employee login
        return Ok(new Response<EmployeeLoginViewModel>
        {
           // StatusCode = 200, Message = "success", Data = employeeService.LoginAsync(model)
        });
    }
}