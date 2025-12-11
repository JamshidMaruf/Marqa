using System.Net.Http.Headers;
using System.Text;
using Marqa.DataAccess.Repositories;
using Marqa.Domain.Entities;
using Marqa.Service.Exceptions;
using Marqa.Service.Services.Employees;
using Marqa.Service.Services.Messages.Models;
using Marqa.Service.Services.Settings;
using Marqa.Service.Services.Students;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Marqa.Service.Services.Messages;

public class SmsService(
    IRepository<OTP> otpRepository,
    ISettingService settingService, 
    IStudentService studentService,
    IEmployeeService employeeService) : ISmsService
{
    public async Task SendOTPAsync(string phone)
    {
        var otp = GenerateSixDigitNumber();

        otpRepository.Insert(new OTP
        {
            PhoneNumber = phone, 
            Code = otp,
            ExpiryDate = DateTime.UtcNow.AddMinutes(2),
        });
        
        await SendMessageAsync(phone, otp);
    }

    public async Task<(int EntityId, string EntityType)> VerifyOTPAsync(string phone, string code)
    {
        var entity = await GetEntity(phone);

        if (entity.Id == 0)
            throw new NotFoundException("This phone number does not exist");
        
        var isVerified = await otpRepository
            .SelectAllAsQueryable(t =>
                t.PhoneNumber == phone &&
                t.Code == code &&
                !t.IsUsed &&
                t.ExpiryDate > DateTime.UtcNow)
            .OrderByDescending(t => t.CreatedAt)
            .AnyAsync();

        if (!isVerified)
            throw new ArgumentIsNotValidException("OTP incorrect");
        
        return (entity.Id, entity.Type);
    }

    private async Task<(int Id, string Type)> GetEntity(string phone)
    {
        int entityId = 0;
        string entityType = "";
        
        int studentId = await studentService.GetByPhoneAsync(phone);
        if (studentId is not default(int))
        {
            entityId = studentId;
            entityType = "Student";
            return (entityId, entityType);
        }

        int teacherId = await employeeService.GetByPhoneAsync(phone);
        if (teacherId is not default(int))
        {
            entityId = teacherId;
            entityType = "Teacher";
            return (entityId, entityType);
        }

        int parentId = await studentService.GetStudentParentByPhoneAsync(phone);
        if (parentId is not default(int))
        {
            entityId = parentId;
            entityType = "Parent";
            return (entityId, entityType);
        }

        return (entityId, entityType);
    }

    private string GenerateSixDigitNumber()
    {
        Random random = new Random();
        var code = random.Next(100000, 1000000).ToString();
        return code;
    }
    
    private async Task<string> LoginAsync()
    {
        var settings = await settingService.GetByCategoryAsync("Eskiz");
        
        var url = settings["Eskiz.LoginUrl"];
        var httpClient = new HttpClient();

        var payload = new SmsPostModel()
        {
            Email = settings["Eskiz.Email"], SecretKey = settings["Eskiz.SecretKey"],
        };
        
        var json = JsonConvert.SerializeObject(payload);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        
        var response = await httpClient.PostAsync(url, content);

        response.EnsureSuccessStatusCode();
        
        var resultJson = await response.Content.ReadAsStringAsync();
        
        var result = JsonConvert.DeserializeObject<LoginResponseModel>(resultJson);
        
        return result.Data.Token;
    }

    public async Task SendNotificationAsync(string phone, string message)
    {
        var settings = await settingService.GetByCategoryAsync("Eskiz");

        var url = settings["Eskiz.SendMessageUrl"];

        var payload = new SendMessageModel
        {
            Phone = phone,
            Message = message,
            From = settings["Eskiz.From"],
        };

        var json = JsonConvert.SerializeObject(payload);

        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var httpClient = new HttpClient();

        var response = await httpClient.PostAsync(url, content);

        response.EnsureSuccessStatusCode();
    }

    private async Task SendMessageAsync(string phone, string otp)
    {
        var settings = await settingService.GetByCategoryAsync("Eskiz");

        var token = await LoginAsync();

        var url = settings["Eskiz.SendMessageUrl"];
        
        var payload = new SendMessageModel
        {
            Phone = phone,
            Message = otp,
            From = settings["Eskiz.From"],
        };
        
        var json = JsonConvert.SerializeObject(payload);
        
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        
        var httpClient = new HttpClient();
        
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        
        var response = await httpClient.PostAsync(url, content);

        response.EnsureSuccessStatusCode();
    }
}