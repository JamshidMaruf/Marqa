﻿using Marqa.Mobile.Student.Api.Models;
using Marqa.Service.Services.Messages;
using Marqa.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Marqa.Mobile.Student.Api.Controllers;

public class AuthController(ISmsService smsService) : BaseController
{
    [AllowAnonymous]
    [HttpPost("session")]
    public IActionResult CreateSession([FromBody] SessionModel model)
    {
        // TODO: Implement token generation
        return Ok(new Response<string>
        {
            StatusCode = 200,
            Message = "token_generated",
            //Data = authService.GenerateAppToken(model.AppId, model.SecretKey)
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
        var result = await smsService.VerifyOTPAsync(phone, code);

        var app = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "App")?.Value;
      //  var token = await authService.GenerateToken(app, result.EntityId, result.EntityType);

        return Ok(new Response<string>
        {
            StatusCode = 200,
            Message = "otp_verified",
        //    Data = token
        });
    }
}
