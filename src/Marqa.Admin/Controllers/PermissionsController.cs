using Marqa.Service.Services.Permissions;
using Marqa.Service.Services.Permissions.Models;
using Marqa.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Marqa.Admin.Controllers;

[Authorize]
public class PermissionsController(IPermissionService permissionService) : Controller
{
    public async Task<IActionResult> Index(PaginationParams? @params, string? search)
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

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(PermissionCreateModel model)
    {
        try
        {
            await permissionService.CreateAsync(model);
        
            return RedirectToAction("Index");
        }
        catch (Exception e)
        {
            ViewBag.ExceptionMessage = e.Message;
            return View();
        }
    }
    
    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var result = await permissionService.GetAsync(id);
        return View(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> Update(int id, PermissionUpdateModel model)
    {
        try
        {
            await permissionService.UpdateAsync(id, model);
        
            return RedirectToAction("Index");
        }
        catch (Exception e)
        {
            ViewBag.ExceptionMessage = e.Message;
            return View();
        }
    }

    public async Task<IActionResult> Delete(int id)
    {
        await permissionService.DeleteAsync(id);
        return RedirectToAction("Index");
    }
}