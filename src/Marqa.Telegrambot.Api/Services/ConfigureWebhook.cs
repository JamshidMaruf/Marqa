using Marqa.Telegrambot.Api.Models;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace Marqa.Telegrambot.Api.Services;

public class ConfigureWebhook(
    ILogger<ConfigureWebhook> logger,
    IServiceProvider serviceProvider,
    IConfiguration configuration) : IHostedService
{
    private readonly BotConfiguration _botConfiguration = 
        configuration.GetSection("BotConfiguration").Get<BotConfiguration>();

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = serviceProvider.CreateScope();

        var botClient = scope.ServiceProvider.GetRequiredService<ITelegramBotClient>();

        var webhookAddress = $@"{_botConfiguration.HostAddress}/bot/{_botConfiguration.Token}";

        logger.LogInformation("Setting webhook...");

        //await botClient.SendMessage(
        //    chatId: 1631057205,
        //    text: "Webhook O'rnatilmoqda...");

        await botClient.SetWebhook(
            url: webhookAddress,
            allowedUpdates: Array.Empty<UpdateType>(),
            cancellationToken: cancellationToken);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        using var scope = serviceProvider.CreateScope();

        var botClient = scope.ServiceProvider.GetRequiredService<ITelegramBotClient>();

        logger.LogInformation("Removing webhook...");

        await botClient.SendMessage(
            chatId: 1631057205,
            text: "Bot uhlamoqda...");
    }
}
