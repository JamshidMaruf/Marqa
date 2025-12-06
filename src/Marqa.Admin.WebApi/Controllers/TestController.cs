using Hangfire;
using Marqa.Service.Services.Messages;
using Microsoft.AspNetCore.Mvc;

namespace Marqa.Admin.WebApi.Controllers;

public class TestController(ISmsService smsService) : ControllerBase
{
    
}