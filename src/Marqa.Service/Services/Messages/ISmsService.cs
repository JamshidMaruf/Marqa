namespace Marqa.Service.Services.Messages;

public interface ISmsService
{
    Task SendOTPAsync(string phone);
    Task<(int EntityId, string EntityType)> VerifyOTPAsync(string phone, string code);
}