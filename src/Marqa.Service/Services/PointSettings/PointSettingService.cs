using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Marqa.DataAccess.UnitOfWork;
using Marqa.Domain.Entities;
using Marqa.Service.Exceptions;
using Marqa.Service.Services.PointSettings.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Marqa.Service.Services.PointSettings;

public class PointSettingService(IUnitOfWork unitOfWork) : IPointSettingService
{
    private string _key = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9";
    public async Task CreateAsync(PointSettingCreateModel model)
    {
        await unitOfWork.PointSettings.InsertAsync(new PointSetting
        {
            Name = model.Name,
            Point = model.Point,
            Description = model.Description,
            Operation = model.Operation
        });
    }

    public async Task UpdateAsync(int id, PointSettingUpdateModel model)
    {
        var pointSetting = await unitOfWork.PointSettings.SelectAsync(p => p.Id == id)
            ?? throw new NotFoundException($"No point_setting was found with ID {id}");

        pointSetting.Name = model.Name;
        pointSetting.Point = model.Point;
        pointSetting.Description = model.Description;
        pointSetting.Operation = model.Operation;

        await unitOfWork.PointSettings.UpdateAsync(pointSetting);
    }

    public async Task DeleteAsync(int id)
    {
        var pointSetting = await unitOfWork.PointSettings.SelectAsync(p => p.Id == id)
            ?? throw new NotFoundException($"No poinit_setting was found with ID = {id}");

        await unitOfWork.PointSettings.DeleteAsync(pointSetting);
    }

    public async Task<PointSettingViewModel> GetAsync(int id)
    {
        var pointSetting = await unitOfWork.PointSettings.SelectAsync(p => p.Id == id);

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
        var pointQuery = unitOfWork.PointSettings
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
        var pointSetting = await unitOfWork.PointSettings.SelectAsync(p => p.Id == id)
            ?? throw new NotFoundException($"No point_setting was found with Id = {id}");

        if (pointSetting.IsEnabled)
            pointSetting.IsEnabled = false;
        else
            pointSetting.IsEnabled = true;

        await unitOfWork.PointSettings.UpdateAsync(pointSetting);
    }


    public string GenerateToken(TokenModel model)
    {
        var serializingOptions = new JsonSerializerOptions();
        serializingOptions.WriteIndented = true;

        string payloadJson = JsonSerializer.Serialize(model, serializingOptions);
        byte[] payloadBytes = Encoding.UTF8.GetBytes(payloadJson);
        string payloadB64 = Base64UrlEncoder.Encode(payloadBytes);

        using HMACSHA3_512 hMACSHA3_512 = new HMACSHA3_512(Encoding.UTF8.GetBytes(_key));
        var signature = hMACSHA3_512.ComputeHash(payloadBytes);
        var sigB64 = Base64UrlEncoder.Encode(signature);

        string token = $"{sigB64}.{payloadB64}";

        return token;
    }

    public TokenModel DecodeToken(string token)
    {
        if (string.IsNullOrWhiteSpace(token))
            throw new ArgumentIsNotValidException("Token must be provided in order to decode it!");

        string[] parts = token.Split('.');

        if (parts.Length != 2)
            throw new ArgumentIsNotValidException("Token is not in the correct format!");

        var sigB64 = parts[0];
        var payloadB64 = parts[1];
        
        // decoding signature and payload
        byte[] decodedPayload = Base64UrlEncoder.DecodeBytes(payloadB64);
        byte[] decodedSignature = Base64UrlEncoder.DecodeBytes(sigB64);

        // hashing the encoded payload in order to compare with the hashed signature
        using HMACSHA3_512 hMACSHA3_512 = new HMACSHA3_512(Encoding.UTF8.GetBytes(_key));
        byte[] expectedSignature = hMACSHA3_512.ComputeHash(decodedPayload);

        // checking for decoded signature and the expected signature for validity
        if (!Equals(decodedSignature.ToString(), expectedSignature.ToString()))
        {
            throw new NotMatchedException("Token was not matched!");
        }

        // deserializing the decodedpayload and if failed throwing exception
        TokenModel tokenModel = JsonSerializer.Deserialize<TokenModel>(decodedPayload)
            ?? throw new ArgumentIsNotValidException("Could not deserialize the token!");

        return tokenModel;
    }
}

