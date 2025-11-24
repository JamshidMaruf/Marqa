using Marqa.Service.Services.Auth;
using Marqa.Service.Services.Auth.Models;
using Marqa.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace Marqa.Admin.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginModel model)
    {
        var ipAddress = HttpContext?.Connection?.RemoteIpAddress?.ToString();
        var data = await authService.LoginAsync(model, ipAddress);
        return Ok(new Response<LoginResponseModel>
        {
            StatusCode = 200, 
            Message = "success",
            Data = data
        });
    }
    
    [HttpPost("logout")]
    public async Task<IActionResult> LogoutAsync([FromBody] LoginModel model)
    {
        var ipAddress = HttpContext?.Connection?.RemoteIpAddress?.ToString();
        var data = await authService.LoginAsync(model, ipAddress);
        return Ok(new Response<LoginResponseModel>
        {
            StatusCode = 200, 
            Message = "success",
            Data = data
        });
    }
    
    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshTokenAsync([FromBody] LoginModel model)
    {
        var ipAddress = HttpContext?.Connection?.RemoteIpAddress?.ToString();
        var data = await authService.LoginAsync(model, ipAddress);
        return Ok(new Response<LoginResponseModel>
        {
            StatusCode = 200, 
            Message = "success",
            Data = data
        });
    }
    
    [HttpPost("current-user")]
    public async Task<IActionResult> GetCurrentUserAsync([FromBody] LoginModel model)
    {
        var ipAddress = HttpContext?.Connection?.RemoteIpAddress?.ToString();
        var data = await authService.LoginAsync(model, ipAddress);
        return Ok(new Response<LoginResponseModel>
        {
            StatusCode = 200, 
            Message = "success",
            Data = data
        });
    }
}