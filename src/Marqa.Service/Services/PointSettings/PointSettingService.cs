using Marqa.DataAccess.Repositories;
using Marqa.Domain.Entities;
using Marqa.Service.Exceptions;
using Marqa.Service.Services.PointSettings.Models;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;

namespace Marqa.Service.Services.PointSettings;

public class PointSettingService(
    IRepository<PointSetting> pointSettingRepository
    /*,IRepository<Student> studentRepository*/) : IPointSettingService
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

    public Task<string> GenerateToken(TokenModel model)
    {
        throw new NotImplementedException();
    }

    public Task<TokenModel> DecodeToken(string token)
    {
        throw new NotImplementedException();
    }
}
