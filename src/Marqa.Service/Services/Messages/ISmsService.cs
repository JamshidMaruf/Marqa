namespace Marqa.Service.Services.Messages;

public interface ISmsService
{
    Task SendOTPAsync(string phone);
    Task<bool> VerifyOTPAsync(string phone, string code);
}