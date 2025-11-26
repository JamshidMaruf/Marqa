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
            Message = "Login successfully",
            Data = data
        });
    }
    
    [HttpPost("logout")]
    public async Task<IActionResult> LogoutAsync([FromBody] LogoutModel model)
    {
        var ipAddress = HttpContext?.Connection?.RemoteIpAddress?.ToString();
        var data = await authService.LogoutAsync(model, ipAddress);
        return Ok(new Response<bool>
        {
            StatusCode = 200, 
            Message = "Revoked successfully",
            Data = data
        });
    }
    
    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshTokenAsync([FromBody] RefreshTokenModel model)
    {
        var data = await authService.RefreshTokenAsync(model);
        return Ok(new Response<LoginResponseModel>
        {
            StatusCode = 200, 
            Message = "Refresh token successfully",
            Data = data
        });
    }
    
    [HttpPost("current-user")]
    public async Task<IActionResult> GetCurrentUserAsync([FromBody] RefreshTokenModel model)
    {
        var data = await authService.GetCurrentUser(model);
        return Ok(new Response<LoginResponseModel.UserData>
        {
            StatusCode = 200, 
            Message = "success",
            Data = data
        });
    }
}