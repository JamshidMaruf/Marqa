﻿using Marqa.Service.Services.Companies;
using Marqa.Service.Services.Companies.Models;
using Marqa.Service.Services.Permissions;
using Marqa.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Marqa.Admin.Controllers;

[Authorize]
public class CompaniesController(ICompanyService companyService) : Controller
{
    public async Task<IActionResult> Index(PaginationParams @params, string search)
    {
        try
        {
            @params ??= new PaginationParams();

            var result = await companyService.GetAllAsync(@params, search);

            var permissionsCount = await companyService.GetCompaniesCountAsync();
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
            return View(new List<CompanyViewModel>());
        }
    }

    [HttpGet]
    public IActionResult Create()
    {
        try
        {
            return View();
        }
        catch
        {
            TempData["ErrorMessage"] = $"Error occured while loading create page";
            return RedirectToAction("Index");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create(CompanyCreateModel model)
    {
        try
        {
            await companyService.CreateAsync(model);
            TempData["SuccessMessage"] = "Company created successfully!";
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = ex.Message;
            return RedirectToAction("Index");
        }
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        try
        {
            var companyDataToUpdate = await companyService.GetForUpdateAsync(id);
            ViewBag.Id = id;
            return View(companyDataToUpdate);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = ex.Message;
            return RedirectToAction("Index");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, CompanyUpdateModel model)
    {
        try
        {
            await companyService.UpdateAsync(id, model);
            TempData["SuccessMessage"] = "Company edited successfully!";
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = ex.Message;
            return RedirectToAction("Index");
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await companyService.DeleteAsync(id);
            TempData["SuccessMessage"] = "Company deleted successfully!";
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
            var result = await companyService.GetAsync(id);

            return PartialView("_Details", result);
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = ex.Message;
            return RedirectToAction("Index");
        }
    }
}