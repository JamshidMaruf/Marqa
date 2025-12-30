﻿namespace Marqa.Service.Services.Messages;

public interface ISmsService : IScopedService
{
    Task SendOTPAsync(string phone);
    Task<(string otp, bool doesExist)> GetOTPForTelegramBotAsync(string phone, long? chatId = null);
    Task<bool> IsExpired(string phone);
    Task<(int EntityId, string EntityType)> VerifyOTPAsync(string phone, string code);
    Task SendNotificationAsync(string phone, string message);
}