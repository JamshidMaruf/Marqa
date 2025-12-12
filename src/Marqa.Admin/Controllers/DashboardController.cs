using Marqa.Service.Services.Companies;
using Marqa.Service.Services.Employees;
using Marqa.Service.Services.Permissions.Models;
using Marqa.Service.Services.Settings;
using Microsoft.AspNetCore.Mvc;

namespace Marqa.Admin.Controllers;

public class DashboardController(
    ICompanyService companyService,
    IEmployeeService employeeService,
    IPermissionService permissionService,
    ISettingService settingService) : Controller
{
    public async Task<IActionResult> Index()
    {
        try
        {
            // Get statistics
            var companies = await companyService.GetAllAsync();

            var employees = await employeeService.GetAllAsync(1);
            var permissions = await permissionService.GetAllAsync();
            var settings = await settingService.GetAllAsync();
            
            ViewBag.TotalCompanies = companies.Count();
            ViewBag.TotalEmployees = employees.Count();
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
