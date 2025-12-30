using Marqa.Telegrambot.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;

namespace Marqa.Telegrambot.Api.Controllers;

[ApiController]
[Route("bot/{token}")]
public class TelegramWebhookController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Handle(
        [FromRoute] string token,
        [FromServices] BotHandler botHandler,
        [FromBody] Update update)
    {
        await botHandler.EchoAsync(update);

        return Ok();
    }
}
