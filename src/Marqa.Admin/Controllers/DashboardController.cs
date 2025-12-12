using Marqa.Service.Services.Companies;
using Marqa.Service.Services.Permissions.Models;
using Marqa.Service.Services.Settings;
using Marqa.Service.Services.Users;
using Microsoft.AspNetCore.Mvc;

namespace Marqa.Admin.Controllers;

public class DashboardController(
    ICompanyService companyService,
    IUserService userService,
    IPermissionService permissionService,
    ISettingService settingService) : Controller
{
    public async Task<IActionResult> Index()
    {
        try
        {
            // Get statistics
            var companies = await companyService.GetAllAsync();
            var usersCount = await userService.GetAllUsersCount();
            var permissions = await permissionService.GetAllAsync();
            var settings = await settingService.GetAllAsync();
            
            ViewBag.TotalCompanies = companies.Count();
            ViewBag.TotalUsers = usersCount;
            ViewBag.TotalSettings = settings.Count();

            return View();
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Error loading dashboard: {ex.Message}";
            ViewBag.TotalCompanies = 0;
            ViewBag.TotalUsers = 0;
            ViewBag.TotalProducts = 0;
            ViewBag.TotalBranches = 0;
            return View();
        }
    }
}
