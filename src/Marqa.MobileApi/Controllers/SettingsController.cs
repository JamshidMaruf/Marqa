using Marqa.MobileApi.Models;
using Marqa.Service.Services.Settings;
using Marqa.Service.Services.Settings.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Marqa.MobileApi.Controllers;

[AllowAnonymous]
public class SettingsController(ISettingService settingService) : BaseController
{
    
    [HttpPost]
    public async Task<IActionResult> Create(SettingCreateModel model)
    {
        await settingService.CreateAsync(model);

        return Ok(new Response
        {
            StatusCode = 200,
            Message = "success",
        });
    }
    
    [HttpDelete("{key}")]
    public async Task<IActionResult> DeleteAsync(string key)
    {
        await settingService.DeleteAsync(key);

        return Ok(new Response
        {
            StatusCode = 200,
            Message = "success",
        });
    }
    
    [HttpGet("{key}")]
    public async Task<IActionResult> GetAsync(string key)
    {
        var setting = await settingService.GetAsync(key);

        return Ok(new Response<SettingViewModel>
        {
            StatusCode = 200,
            Message = "success",
            Data = setting
        });
    }
    
    [HttpGet]
    public async Task<IActionResult> GetByCategoryAsync(string category)
    {
        var settings = await settingService.GetByCategoryAsync(category);

        return Ok(new Response<Dictionary<string, string>>
        {
            StatusCode = 200,
            Message = "success",
            Data = settings
        });
    }
}