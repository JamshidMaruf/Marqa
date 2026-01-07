using Marqa.Service.Services.Companies;
using Marqa.Service.Services.Permissions;
using Marqa.Service.Services.Settings;
using Marqa.Service.Services.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Marqa.Admin.Controllers;

[Authorize]
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
            var permissionsCount = await permissionService.GetPermissionsCountAsync();
            var settings = await settingService.GetAllAsync();
            //var subscribers = await subscriptionService.GetAllAsync();    
            
            ViewBag.TotalCompanies = companies.Count();
            ViewBag.TotalUsers = usersCount;
            ViewBag.TotalSettings = settings.Count();
            ViewBag.TotalPermissions = permissionsCount;
            //ViewBag.TotalSubscribers = subscribers.Count();

            return View();
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Error loading dashboard: {ex.Message}";
            ViewBag.TotalCompanies = 0;
            ViewBag.TotalUsers = 0;
            ViewBag.TotalSettings = 0;
            ViewBag.TotalPermissions = 0;
            ViewBag.DatabaseCondition = "problematic";
            //ViewBag.TotalSubscribers = 0;
            return View();
        }
    }
}
