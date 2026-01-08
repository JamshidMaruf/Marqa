using Marqa.Service.Services.Companies;
using Marqa.Service.Services.Permissions;
using Marqa.Service.Services.Permissions.Models;
using Marqa.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Marqa.Admin.Controllers;

[Authorize]
public class PermissionsController(IPermissionService permissionService) : Controller
{
    public async Task<IActionResult> Index(PaginationParams @params, string search)
    {
        try
        {
            @params ??= new PaginationParams();

            var result = await permissionService.GetAllAsync(@params, search);

            var permissionsCount = await permissionService.GetPermissionsCountAsync();
            var totalPages = (int)Math.Ceiling(permissionsCount / (double)@params.PageSize);

            ViewBag.CurrentPage = @params.PageNumber;
            ViewBag.PageSize = @params.PageSize;
            ViewBag.TotalPages = totalPages == 0 ? 1 : totalPages;
            ViewBag.Search = search;

            return View(result);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"Error loading companies: {ex.Message}";
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
    public async Task<IActionResult> Create(PermissionCreateModel model)
    {
        try
        {
            await permissionService.CreateAsync(model);
            TempData["SuccessMessage"] = "Permission created successfully!";

            return RedirectToAction("Index");
        }
        catch (FluentValidation.ValidationException ex)
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

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        try
        {
            var result = await permissionService.GetForUpdateFormAsync(id);
            ViewBag.Id = id;
            return View(result);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = ex.Message;
            return RedirectToAction("Index");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, PermissionUpdateModel model)
    {
        try
        {
            await permissionService.UpdateAsync(id, model);
            TempData["SuccessMessage"] = "Permission edited successfully!";
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = ex.Message;
            return View();
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await permissionService.DeleteAsync(id);
            TempData["SuccessMessage"] = "Permission deleted successfully!";
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = ex.Message;
            return View();
        }
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        try
        {
            var result = await permissionService.GetAsync(id);

            return PartialView("_Details", result);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = ex.Message;
            return RedirectToAction("Index");
        }
    }
}
