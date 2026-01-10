using Marqa.Service.Services.Permissions.Models;
using Marqa.Service.Services.Users;
using Marqa.Service.Services.Users.Models;
using Marqa.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Marqa.Admin.Controllers;

[Authorize]
public class UsersController(IUserService userService) : Controller
{
    public async Task<IActionResult> Index(PaginationParams @params, string search)
    {
        try
        {
            @params ??= new PaginationParams();

            var result = await userService.GetAllAsync(@params, search);

            var usersCount = await userService.GetAllUsersCount();
            var totalPages = (int)Math.Ceiling(usersCount / (double)@params.PageSize);

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

    [HttpPatch]
    public async Task<IActionResult> ChangeBlockStatus(int id)
    {
        try
        {
            var isBlocked = await userService.SetBlockStatusAsync(id);

            if (isBlocked)
                TempData["SuccessMessage"] = "User blocked successfully";
            else
                TempData["SuccessMessage"] = "User unblocked successfully";

            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = ex.Message;
            return RedirectToAction("Index");
        }
    }

    [HttpPatch]
    public async Task<IActionResult> ChangePassword(int id, UserPasswordModel model)
    {
        try
        {
            await userService.ChangePasswordAsync(id, model);

            TempData["SuccessMessage"] = "User's password updated successfully";

            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = ex.Message;
            return RedirectToAction("Index");
        }
    }


    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        try
        {
            var result = await userService.GetAsync(id);

            return PartialView("_Details", result);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = ex.Message;
            return RedirectToAction("Index");
        }
    }
}
