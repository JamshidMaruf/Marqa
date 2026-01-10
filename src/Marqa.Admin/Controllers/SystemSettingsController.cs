using Marqa.Service.Services.Permissions.Models;
using Marqa.Service.Services.Settings;
using Marqa.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Marqa.Admin.Controllers;

[Authorize]
public class SystemSettingsController(ISettingService settingService) : Controller
{
    public async Task<IActionResult> Index(PaginationParams @params, string search)
    {
        try
        {
            @params ??= new PaginationParams();

            var result = await settingService.GetAllAsync(@params, search);

            var permissionsCount = await settingService.GetSettingsCountAsync();
            var totalPages = (int)Math.Ceiling(permissionsCount / (double)@params.PageSize);

            ViewBag.CurrentPage = @params.PageNumber;
            ViewBag.PageSize = @params.PageSize;
            ViewBag.TotalPages = totalPages == 0 ? 1 : totalPages;
            ViewBag.Search = search;

            return View(result);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Error loading system_settings: {ex.Message}";
            return View(new List<PermissionViewModel>());
        }
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

    [HttpGet]
    public async Task<IActionResult> Details(string key)
    {
        try
        {
            var result = await settingService.GetAsync(key);

            return PartialView("_Details", result);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = ex.Message;
            return RedirectToAction("Index");
        }
    }

}
