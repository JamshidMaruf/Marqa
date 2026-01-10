using FluentValidation;
using Marqa.Service.Services.Permissions.Models;
using Marqa.Service.Services.Settings;
using Marqa.Service.Services.Settings.Models;
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

    [HttpGet]
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

    [HttpPost]
    public async Task<IActionResult> Create(SettingCreateModel model)
    {
        try
        {
            await settingService.CreateAsync(model);
            TempData["SuccessMessage"] = "System-setting created successfully!";

            return RedirectToAction("Index");
        }
        catch (ValidationException ex)
        {
            foreach (var error in ex.Errors)
            {
                ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }

            return View(model);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = ex.Message;
            return View();
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(string key)
    {
        try
        {
            await settingService.DeleteAsync(key);
            TempData["SuccessMessage"] = "System-setting deleted successfully!";
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = ex.Message;
            return View();
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
