using Marqa.Telegrambot.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;

namespace Marqa.Telegrambot.Api.Controllers;

[ApiController]
[Route("telegram")]
public class TelegramWebhookController : ControllerBase
{
    [HttpPost("webhook")]
    public async Task<IActionResult> Handle([FromServices] BotHandler botHandler,
        [FromQuery] Update update)
    {
        await botHandler.EchoAsync(update);

        return Ok();
    }
}