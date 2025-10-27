using Marqa.MobileApi.Models;
using Marqa.Service.Services.Auth;
using Marqa.Service.Services.Messages;
using Marqa.Service.Services.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Marqa.MobileApi.Controllers;

public class AuthController(IAuthService authService, ISmsService smsService) : BaseController
{
    [AllowAnonymous]
    [HttpPost("session")]
    public async Task<IActionResult> CreateSessionAsync([FromBody] SessionModel model)
    {
        return Ok(new Response<string>
        {
            StatusCode = 200,
            Message = "token_generated",
            Data = await authService.GenerateAppToken(model.AppId, model.SecretKey)
        });
    }
    
    [HttpPost("otp/send")]
    public async Task<IActionResult> SentOTPAsync(string phone)
    {
        await smsService.SendOTPAsync(phone);

        return Ok(new Response
        {
            StatusCode = 200,
            Message = "otp_sent",
        });
    }

    [HttpGet("otp/verify")]
    public async Task<IActionResult> VerifyOTPAsync(string phone, string code)
    {
        await smsService.VerifyOTPAsync(phone, code);
        var app = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "App")?.Value;
        var token = await authService.GenerateToken(app);

        return Ok(new Response<string>
        {
            StatusCode = 200,
            Message = "otp_verified",
            Data = token
        });
    }
}