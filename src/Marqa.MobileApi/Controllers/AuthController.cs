using Marqa.MobileApi.Models;
using Marqa.Service.Services.Auth;
using Marqa.Service.Services.Messages;
using Microsoft.AspNetCore.Mvc;

namespace Marqa.MobileApi.Controllers;

public class AuthController(IAuthService authService, ISmsService smsService, IConfiguration configuration) : BaseController
{
    [HttpPost("session")]
    public IActionResult CreateSessionAsync([FromBody] SessionModel model)
    {
        if (configuration["Mobile:UserId"] == model.UserId &&
            configuration["Mobile:SecretKey"] == model.SecretKey)
        {
            return Ok(new Response<string>
            {
                StatusCode = 200,
                Message = "token_generated",
                Data = authService.GenerateToken(-1, model.UserId ,"Mobile")
            });
        }

        return BadRequest(new Response
        {
            StatusCode = 403,
            Message = "Forbiden"
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

    public async Task<IActionResult> VerifyOTPAsync(string phone, string code)
    {
        await smsService.VerifyOTPAsync(phone, code);

        return Ok(new Response() { StatusCode = 200, Message = "otp_verified" });
    }
}