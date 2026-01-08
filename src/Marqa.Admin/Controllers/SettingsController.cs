using Marqa.Service.Services.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Marqa.Admin.Controllers;

[Authorize]
public class SettingsController(ISettingService settingService) : Controller
{
    public async Task<IActionResult> Index()
    {
        var result = await settingService.GetAllAsync();
        return View(result);
    }

    [HttpPost]
    public IActionResult Create()
    {
        try
        {
            return View();
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = ex.Message;
            return View("Index");
        }
    }
}
