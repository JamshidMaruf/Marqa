using Marqa.Service.Services.Settings;
using Microsoft.AspNetCore.Mvc;

namespace Marqa.Admin.Controllers;

public class SettingsController(ISettingService settingService) : Controller
{
    public async Task<IActionResult> Index()
    {
        var result = await settingService.GetAllAsync();
        return View(result);
    }
}
