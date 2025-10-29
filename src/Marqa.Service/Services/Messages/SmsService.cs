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
        
        return (entity.Id, entity.Type);
    }

    private async Task<(int Id, string Type)> GetEntity(string phone)
    {
        int entityId = 0;
        string entityType = "";
        
        var student = await studentService.GetByPhoneAsync(phone);
        var teacher = await employeeService.GetByPhoneAsync(phone);
        var parent = await studentService.GetStudentParentByPhoneAsync(phone);
        
        if (student != null)
        {
            entityId = student.Id;
            entityType = "Student";
        }
        else if (teacher != null)
        {
            entityId = teacher.Id;
            entityType = "Teacher";
        }
        else if (parent != null)
        {
            entityId = parent.Id;
            entityType = "Parent";
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