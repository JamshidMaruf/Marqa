using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Marqa.DataAccess.Repositories;
using Marqa.Domain.Entities;
using Marqa.Service.Exceptions;
using Marqa.Service.Services.PointSettings.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Marqa.Service.Services.PointSettings;

public class PointSettingService(
    IRepository<PointSetting> pointSettingRepository) : IPointSettingService
{
    public async Task CreateAsync(PointSettingCreateModel model)
    {
        await pointSettingRepository.InsertAsync(new PointSetting
        {
            Name = model.Name,
            Point = model.Point,
            Description = model.Description,
            Operation = model.Operation
        });
    }

    public async Task UpdateAsync(int id, PointSettingUpdateModel model)
    {
        var pointSetting = await pointSettingRepository.SelectAsync(id)
            ?? throw new NotFoundException($"No point_setting was found with ID {id}");

        pointSetting.Name = model.Name;
        pointSetting.Point = model.Point;
        pointSetting.Description = model.Description;
        pointSetting.Operation = model.Operation;

        await pointSettingRepository.UpdateAsync(pointSetting);
    }

    public async Task DeleteAsync(int id)
    {
        var pointSetting = await pointSettingRepository.SelectAsync(id)
            ?? throw new NotFoundException($"No poinit_setting was found with ID = {id}");

        await pointSettingRepository.DeleteAsync(pointSetting);
    }

    public async Task<PointSettingViewModel> GetAsync(int id)
    {
        var pointSetting = await pointSettingRepository.SelectAsync(id);

        return new PointSettingViewModel
        {
            Name = pointSetting.Name,
            Point = pointSetting.Point,
            Description = pointSetting.Description,
            Operation = pointSetting.Operation
        };
    }

    public async Task<IEnumerable<PointSettingViewModel>> GetAllAsync(string search = null)
    {
        var pointQuery = pointSettingRepository
            .SelectAllAsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            pointQuery = pointQuery
                .Where(p => p.Name.Contains(search) ||
                       p.Description.Contains(search) ||
                       p.Point.ToString().Contains(search) ||
                       p.Operation.ToString().Contains(search));
        }

        return await pointQuery.Select(p => new PointSettingViewModel
        {
            Name = p.Name,
            Point = p.Point,
            Description = p.Description,
            Operation = p.Operation
        }).ToListAsync();
    }

    public async Task ToggleAsync(int id)
    {
        var pointSetting = await pointSettingRepository.SelectAsync(id)
            ?? throw new NotFoundException($"No point_setting was found with Id = {id}");

        if (pointSetting.IsEnabled)
            pointSetting.IsEnabled = false;
        else
            pointSetting.IsEnabled = true;

        await pointSettingRepository.UpdateAsync(pointSetting);
    }

    public string GenerateToken(TokenModel model)
    {
        string key = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9";
        byte[] keyBytes = Encoding.UTF8.GetBytes(key);

        var serializingOptions = new JsonSerializerOptions();
        serializingOptions.WriteIndented = true;

        string payloadJson = JsonSerializer.Serialize(model, serializingOptions);
        byte[] payloadBytes = Encoding.UTF8.GetBytes(payloadJson);
        string payloadB64 = Base64UrlEncoder.Encode(payloadBytes);
        
        using HMACSHA3_512 hMACSHA3_512 = new HMACSHA3_512(keyBytes);
        var signature = hMACSHA3_512.ComputeHash(Encoding.UTF8.GetBytes(payloadB64));
        var sigB64 = Base64UrlEncoder.Encode(signature);

        string token = $"{sigB64}.{payloadB64}";

        return token;
    }

    public TokenModel DecodeToken(string token)
    {
        if (string.IsNullOrWhiteSpace(token))
            return null;

        string[] parts = token.Split('.');

        if(parts.Length != 2) return null;

        var sigB64 = parts[0];
        var payloadB64 = parts[1];

        byte[] decodedSignature;
        try
        {
            decodedSignature = Base64UrlEncoder.DecodeBytes(sigB64);
        }
        catch
        {
            return null;
        }
        byte[] decodedPayload;
        try
        {
            decodedPayload = Base64UrlEncoder.DecodeBytes(payloadB64);
        }
        catch
        {
            return null;
        }

        var s = JsonSerializer.Deserialize<string>(decodedSignature);
        var p = JsonSerializer.Deserialize<TokenModel>(decodedSignature);

        return p;
    }
}

