using Marqa.Service.Services.Companies;
using Marqa.Service.Services.Permissions;
using Marqa.Service.Services.Permissions.Models;
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
            var permissions = await permissionService.GetAllAsync();
            var settings = await settingService.GetAllAsync();
            //var subscribers = await subscriptionService.GetAllSubscribersAsync();    
            
            ViewBag.TotalCompanies = companies.Count();
            ViewBag.TotalUsers = usersCount;
            ViewBag.TotalSettings = settings.Count();
            //ViewBag.TotalSubscribers = subscribers.Count();

            return View();
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Error loading dashboard: {ex.Message}";
            ViewBag.TotalCompanies = 0;
            ViewBag.TotalUsers = 0;
            ViewBag.TotalSettings = 0;
            //ViewBag.TotalSubscribers = 0;
            return View();
        }
    }
}
