//using Marqa.Telegrambot.Api.Services;
//using Microsoft.AspNetCore.Mvc;
//using Telegram.Bot.Types;

//namespace Marqa.Telegrambot.Api.Controllers;

//[ApiController]
//[Route("bot/{token}")]
//public class TelegramWebhookController : ControllerBase
//{
//    [HttpPost]
//    public async Task<IActionResult> Handle(
//        [FromRoute] string token,
//        [FromServices] BotHandler botHandler,
//        [FromBody] Update update)
//    {
//        await botHandler.EchoAsync(update);

//        return Ok();
//    }
//}

using Marqa.Telegrambot.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;
using System.Text.Json;

[ApiController]
[Route("bot/{token}")]
public class TelegramWebhookController : ControllerBase
{
    private readonly ILogger<TelegramWebhookController> _logger;

    public TelegramWebhookController(ILogger<TelegramWebhookController> logger)
    {
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> Handle(
        [FromRoute] string token,
        [FromServices] BotHandler botHandler)
    {
        // Read raw body (for debugging)
        Request.EnableBuffering();
        using var sr = new StreamReader(Request.Body, leaveOpen: true);
        var body = await sr.ReadToEndAsync();
        Request.Body.Position = 0;

        _logger.LogInformation("Raw webhook body: {body}", body);

        // Try to deserialize with System.Text.Json (case-insensitive)
        Update? update = null;
        try
        {
            var opts = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            update = JsonSerializer.Deserialize<Update>(body, opts);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "System.Text.Json failed to deserialize Update.");
        }

        if (update is null)
        {
            // fallback: return 200 to avoid Telegram retry storms, but log clearly
            _logger.LogWarning("Update was null after deserialization — returning 200 to stop retries. Check raw body above.");
            return Ok();
        }

        await botHandler.EchoAsync(update);
        return Ok();
    }
}
