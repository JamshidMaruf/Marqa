using System.Security;
using Marqa.Service.Exceptions;
using Marqa.Service.Services.Permissions.Models;
using Microsoft.AspNetCore.Mvc;

namespace Marqa.Admin.Controllers;

public class PermissionsController(IPermissionService permissionService) : Controller
{
    public async Task<IActionResult> Index()
    {
        var result = await permissionService.GetAllAsync();
        
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