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
            await HandleErrorAsync(ex, update);
        }
    }

    public async Task HandleErrorAsync(Exception ex, Update update)
    {
        logger.LogError(ex.Message);

        await botClient.SendMessage(
               chatId: update.Message.Chat.Id,
               text: $"{ex.Message}",
               parseMode: ParseMode.Markdown);
    }

    private async Task BotOnMessageRecievedAsync(Message message)
    {
        logger.LogInformation($"Message type: {message.Type}");

        long chatId = message.Chat.Id;

        string text = message.Text != null ? message.Text.Trim() : string.Empty;

        if (text.ToLower() == "/start")
        {
            string userFirstName = message.From.FirstName;
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
    }

    private async Task BotOnCallbackQueryRecievedAsync(CallbackQuery callbackQuery)
    {
        if (callbackQuery.Message is not null)
        {
            long chatId = callbackQuery.From.Id;
            string callbackId = callbackQuery.Id;
            string message = callbackQuery.Data;

            if (message == "refresh_login")
            {
                await HandleRefreshLoginAsync(chatId, callbackId, message);
            }
        }
    }

    private async Task BotOnUnknownTypeRecievedAsync(Update update)
    {
        logger.LogInformation($"unknown update type recieved {update.Type}");

        await Task.CompletedTask;
    }

    #region MessageChunks
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
                        InlineKeyboardButton.WithUrl(
                            "Login",
                            "https://taleem.uz/login"
                        ),
                        InlineKeyboardButton.WithCallbackData(
                            "🔄 Yangilash",
                            "refresh_login"
                        )
                    );

                    var msg = await botClient.SendMessage(
                        chatId: chatId,
                        text: $"🔒 Code: `{result.otp}`\n" +
                               "🔗 Click and Login:\n" +
                               "https://taleem.uz/login",
                        replyMarkup: keyboard,
                        parseMode: ParseMode.Markdown);
                    return;


                }
                else
                {
                    await botClient.SendMessage(
                        chatId: chatId,
                        text: "❌ You are not unknown to this platform!" +
                              "❌ Вы не зарегистрированы на платформе.",
                        parseMode: ParseMode.Markdown);
                    return;
                }
            }
            else
            {
                await botClient.SendMessage(
                    chatId: chatId,
                    text:
                        $"Eski kodingizni hali ham amal qiladi!\n" +
                        $"Agar yangi kod olishni istasangiz 2 daqiqa kuting!",
                    parseMode: ParseMode.Markdown);
                return;
            }
        }
        else
        {
            await botClient.SendMessage(
                chatId: chatId,
                text: "❌ You are unknown to this platform!" +
                      "❌ Вы не зарегистрированы на платформе.",
                parseMode: ParseMode.Markdown);
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
                   $"Это не ваш номер телефона!",
               replyMarkup: new ReplyKeyboardRemove(),
               parseMode: ParseMode.Markdown);
            return;
        }

        if (await smsService.IsExpired(phone))
        {
            var result = await smsService.GetOTPForTelegramBotAsync(phone, chatId);

            if (result.doesExist)
            {
                await botClient.SendMessage(
                    chatId: chatId,
                    text: $"🔒 Code: `{result.otp}`\n" +
                          "🔗 Click and Login: \n" +
                          "https://taleem.uz/login",
                    replyMarkup: new ReplyKeyboardRemove(),
                    parseMode: ParseMode.Markdown);

                await botClient.SendMessage(
                    chatId: chatId,
                    text: $"🇺🇿\n🔑 Yangi kod olish uchun /login ni bosing" +
                          $"🇷🇺\n🔑 Чтобы получить новый код, нажмите /login",
                    parseMode: ParseMode.Markdown);
            }
            else
            {
                await botClient.SendMessage(
                    chatId: chatId,
                    text: "❌ You are unknown to this platform!" +
                          "❌ Вы не зарегистрированы на платформе.",
                    replyMarkup: new ReplyKeyboardRemove(),
                    parseMode: ParseMode.Markdown);
            }
        }
        else
        {
            await botClient.SendMessage(
                chatId: chatId,
                text:
                    $"Eski kodingizni hali ham amal qiladi!\n" +
                    $"Agar yangi kod olishni istasangiz 2 daqiqa kuting!",
                parseMode: ParseMode.Markdown);
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
                    $"Salom {userFirstName} 😊\n" +
                    $"Taleem platformasining rasmiy botiga xush kelibsiz!\n" +
                    $"⬇️ Kontaktingizni yuboring\n\n" +
                    $"🇷🇺" +
                    $"Привет, {userFirstName} 😊\n" +
                    $"Добро пожаловать в официальный бот платформы Taleem!\n" +
                    $"⬇️ Отправьте ваш контакт",
                    parseMode: ParseMode.Markdown,
                    replyMarkup: keyboard);
    }
    #endregion

    #region CallbackQueryChunks
    private async Task HandleRefreshLoginAsync(long chatId, string callbackId, string message)
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
                        InlineKeyboardButton.WithUrl(
                            "Login",
                            "https://taleem.uz/login"
                        ),
                        InlineKeyboardButton.WithCallbackData(
                            "🔄 Yangilash",
                            "refresh_login"
                        )
                    );

                    await botClient.SendMessage(
                        chatId: chatId,
                        text: $"🔒 Code: `{result.otp}`\n" +
                               "🔗 Click and Login:\n" +
                               "https://taleem.uz/login",
                        replyMarkup: keyboard,
                        parseMode: ParseMode.Markdown);
                    return;
                }
                else
                {
                    await botClient.SendMessage(
                        chatId: chatId,
                        text: @"❌ Siz platformada ro'yxatdan o'tmagansiz!" +
                               "❌ Вы не зарегистрированы на платформе.",
                        parseMode: ParseMode.Markdown);
                    return;
                }
            }
            else
            {
                await botClient.AnswerCallbackQuery(
                    callbackQueryId: callbackId,
                    text:
                        "Eski kodingizni hali ham amal qiladi!\n" +
                        "Agar yangi kod olishni istasangiz 2 daqiqa kuting!",
                    showAlert: true
                );
                return;
            }
        }
        else
        {
            await botClient.SendMessage(
                chatId: chatId,
                text: "❌ Hmm, biz sizni malumotingizni topa olmadik!" +
                      "❌ К сожалению, нам не удалось найти информацию о вас.",
                parseMode: ParseMode.Markdown);
            return;
        }
    }
    #endregion
}
