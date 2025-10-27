﻿using Marqa.DataAccess.UnitOfWork;
using Marqa.Domain.Entities;
using Marqa.Service.Exceptions;
using Marqa.Service.Services.Settings.Models;
using Microsoft.EntityFrameworkCore;

namespace Marqa.Service.Services.Settings;

public class SettingService(IUnitOfWork unitOfWork, IEncryptionService encryptionService) : ISettingService
{
    public async Task CreateAsync(SettingCreateModel model)
    {
        string value = model.IsEncrypted ? encryptionService.Encrypt(model.Value) : model.Value;
        
        unitOfWork.Settings.Insert(new Setting()
        {
            Key = model.Key,
            Value = value,
            Category = model.Category,
            IsEncrypted = model.IsEncrypted
        });
        
        await unitOfWork.SaveAsync();
    }

    public async Task DeleteAsync(string key)
    {
        var setting = await unitOfWork.Settings.SelectAsync(s => s.Key == key)
            ?? throw new NotFoundException("Setting not found");

        unitOfWork.Settings.Delete(setting);
        
        await unitOfWork.SaveAsync();
    }

    public async Task<SettingViewModel> GetAsync(string key)
    {
        var setting = await unitOfWork.Settings.SelectAsync(s => s.Key == key) 
            ?? throw new NotFoundException("Setting not found");

        if(setting.IsEncrypted)
            setting.Value = encryptionService.Decrypt(setting.Value);

        return new SettingViewModel { Key = setting.Key, Value = setting.Value, Category = setting.Category };
    }

    public async Task<Dictionary<string, string>> GetByCategoryAsync(string category)
    {
        var settings = await unitOfWork.Settings.SelectAllAsQueryable()
            .Where(s => s.Category == category)
            .ToListAsync();

        foreach (var setting in settings)
        {
            if(setting.IsEncrypted)
                setting.Value = encryptionService.Decrypt(setting.Value);
        }

        return settings.ToDictionary(s => s.Key, s => s.Value);
    }
}