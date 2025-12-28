using Marqa.Service.Services.Messages;
using Marqa.Service.Services.Users;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Marqa.Telegrambot.Api.Services;

public class BotHandler(ILogger<BotHandler> logger,
    ITelegramBotClient botClient,
    ISmsService smsService,
    IUserService userService)
{
    public async Task EchoAsync(Update update)
    {
        var handler = update.Type switch
        {
            UpdateType.Message => BotOnMessageRecievedAsync(update.Message),

            UpdateType.CallbackQuery => BotOnCallbackQueryRecievedAsync(update.CallbackQuery),

            _ => BotOnUnknownTypeRecievedAsync(update)
        };

        try
        {
            await handler;
        }
        catch (Exception ex)
        {
            await HandleErrorAsync(ex);
        }
    }

    public async Task HandleErrorAsync(Exception ex)
    {
        throw new NotImplementedException();
    }

    private async Task BotOnMessageRecievedAsync(Message message)
    {
        logger.LogInformation($"Message type: {message.Type}");

        long chatId = message.Chat.Id;
        string userFirstName = message.From.FirstName;
        string text = message.Text.Trim();

        if (text.ToLower() == "/start" || text.ToLower() == "/restart")
        {
            await HandleStartCommandAsync(chatId, userFirstName);
        }
        else if (message.Contact != null)
        {
            await SendOTPOnContactSharedAsync(message, chatId);
        }
        else if (text.ToLower() == "/login")
        {
            await HandleLoginCommandAsync(chatId);
        }
        else
        {
            await botClient.SendMessage(
               chatId: chatId,
               text: "sizni tushunmadim 🤔",
               parseMode: ParseMode.Markdown);
            return;
        }
    }

    private async Task HandleLoginCommandAsync(long chatId)
    {
        var phone = await userService.GetUserPhoneByChatIdAsync(chatId);

        if (phone != null)
        {
            if (await smsService.IsExpired(phone))
            {
                var result = await smsService.GetOTPForTelegramBotAsync(phone);

                if (result.doesExist)
                {
                    var keyboard = new InlineKeyboardMarkup(
                        InlineKeyboardButton.WithCallbackData(
                            "Login", "login;https:/taleem.uz/login"
                        ),
                        InlineKeyboardButton.WithCallbackData(
                            "Yangilash", $"{HandleLoginCommandAsync(chatId)}"
                        )  
                    );

                    await botClient.SendMessage(
                        chatId: chatId,
                        text: $"🔒 Code: `{result.otp}`" +
                              "🔗 Click and Login: https:/taleem.uz",
                        replyMarkup: keyboard,
                        parseMode: ParseMode.MarkdownV2);
                    return;
                }
                else
                {
                    await botClient.SendMessage(
                        chatId: chatId,
                        text: $"❌ You are not unknown to this platform!",
                        parseMode: ParseMode.MarkdownV2);
                    return;
                }
            }
            else
            {
                await botClient.SendMessage(
                    chatId: chatId,
                    text: 
                        $"Eski kodingizni hali ham ishlatishingiz mumkin!\n" +
                        $"Agar yangi kod olishni istasangiz 2 daqiqa kuting!",
                    parseMode: ParseMode.MarkdownV2);
                return;
            }
        }
        else
        {
            await botClient.SendMessage(
                chatId: chatId,
                text: $"❌ You are unknown to this platform!",
                parseMode: ParseMode.MarkdownV2);
            return;
        }
    }

    private async Task SendOTPOnContactSharedAsync(Message message, long chatId)
    {
        var phone = message.Contact.PhoneNumber;
        var telegramUserId = message.Contact.UserId;

        if (telegramUserId != message.From.Id)
        {
            await botClient.SendMessage(
               chatId: chatId,
               text:
                   $"Bu sizning telefon raqamingiz emas!\n" +
                   $"This is not your phone number!\n" +
                   $"Это не ваш номер телефона!",
               parseMode: ParseMode.Markdown);
            return;
        }

        var result = await smsService.GetOTPForTelegramBotAsync(phone);

        if (result.doesExist)
        {
            await botClient.SendMessage(
                chatId: chatId,
                text: $"`{result.otp}`",
                parseMode: ParseMode.MarkdownV2);

            await botClient.SendMessage(
                chatId: chatId,
                text: $"🇺🇿\n🔑 Yangi kod olish uchun /login ni bosing" +
                      $"🇺🇸\n🔑 To get a new code click /login" +
                      $"🇷🇺\n🔑 Чтобы получить новый код, нажмите /login",
                parseMode: ParseMode.MarkdownV2);
            return;
        }
        else
        {
            await botClient.SendMessage(
                chatId: chatId,
                text: $"❌ You are unknown to this platform!",
                parseMode: ParseMode.MarkdownV2);
            return;
        }
    }

    private async Task HandleStartCommandAsync(long chatId, string userFirstName)
    {
        var keyboard = new ReplyKeyboardMarkup(new[]
            {
                new[]
                {
                    KeyboardButton.WithRequestContact("📱 YUBORISH/SEND/Неверно")
                }
            })
        {
            ResizeKeyboard = true,
            OneTimeKeyboard = true
        };

        await botClient.SendMessage(
                    chatId: chatId,
                    text: $"" +
                    $"🇺🇿" +
                    $"Salom {userFirstName} 😊" +
                    $"Taleem platformasining rasmiy botiga xush kelibsiz!\n" +
                    $"⬇️ Kontaktingizni yuboring\n" +
                    $"🇺s " +
                    $"Hi {userFirstName} 😊" +
                    $"Welcome to Taleem platform's offical bot!\n" +
                    $"⬇️ Kontaktingizni yuboring\n" +
                    $"🇷🇺" +
                    $"Привет, {userFirstName} 😊\n" +
                    $"Добро пожаловать в официальный бот платформы Taleem!\n" +
                    $"⬇️ Отправьте ваш контакт",
                    parseMode: ParseMode.MarkdownV2,
                    replyMarkup: keyboard);
    }

    private async Task BotOnCallbackQueryRecievedAsync(CallbackQuery callbackQuery)
    {
        throw new NotImplementedException();
    }

    private async Task BotOnUnknownTypeRecievedAsync(Update update)
    {
        throw new NotImplementedException();
    }
}
