using System.Net.Http.Headers;
using System.Text;
using Marqa.DataAccess.Repositories;
using Marqa.Domain.Entities;
using Marqa.Service.Exceptions;
using Marqa.Service.Services.Messages.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Marqa.Service.Services.Messages;

public class SmsService(IConfiguration configuration, IRepository<OTP> otpRepository) : ISmsService
{
    public async Task SendOTPAsync(string phone)
    {
        var otp = GenerateSixDigitNumber();

        await otpRepository.InsertAsync(new OTP
        {
            PhoneNumber = phone, 
            Code = otp,
            ExpiryDate = DateTime.UtcNow.AddMinutes(2),
        });
        
        await SendMessageAsync(phone, otp);
    }

    public async Task<bool> VerifyOTPAsync(string phone, string code)
    {
        var isVerified = await otpRepository
            .SelectAllAsQueryable()
            .Where(t =>
                t.PhoneNumber == phone &&
                t.Code == code &&
                !t.IsUsed &&
                t.ExpiryDate > DateTime.UtcNow)
            .OrderByDescending(t => t.CreatedAt)
            .AnyAsync();

        if (!isVerified)
            throw new ArgumentIsNotValidException("OTP incorrect");

        return isVerified;
    }

    private string GenerateSixDigitNumber()
    {
        Random random = new Random();
        var code = random.Next(100000, 1000000).ToString();
        return code;
    }

    private async Task<string> LoginAsync()
    {
        var url = "https://notify.eskiz.uz/api/auth/login";
        var httpClient = new HttpClient();

        var payload = new SmsPostModel()
        {
            Email = configuration["Eskiz:Email"], SecretKey = configuration["Eskiz:SecretKey"],
        };
        
        var json = JsonConvert.SerializeObject(payload);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        
        var response = await httpClient.PostAsync(url, content);

        response.EnsureSuccessStatusCode();
        
        var resultJson = await response.Content.ReadAsStringAsync();
        
        var result = JsonConvert.DeserializeObject<LoginResponseModel>(resultJson);
        
        return result.Data.Token;
    }

    private async Task SendMessageAsync(string phone, string otp)
    {
        var token = await LoginAsync();

        var url = "https://notify.eskiz.uz/api/message/sms/send";
        
        var payload = new SendMessageModel
        {
            Phone = phone,
            Message = otp,
            From = configuration["Eskiz:From"],
        };
        
        var json = JsonConvert.SerializeObject(payload);
        
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        
        var httpClient = new HttpClient();
        
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        
        var response = await httpClient.PostAsync(url, content);

        response.EnsureSuccessStatusCode();
    }
}