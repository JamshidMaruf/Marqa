﻿using Marqa.Service.Services.Companies;
using Marqa.Service.Services.Companies.Models;
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
            TempData["Error"] = $"Error loading companies: {ex.Message}";
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
            TempData["Error"] = $"Error occured while loading create page";
            return RedirectToAction("Index");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create(CompanyCreateModel model)
    {
        try
        {
            await companyService.CreateAsync(model);
            TempData["Success"] = "Company created successfully!";
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
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
            TempData["Error"] = ex.Message;
            return RedirectToAction("Index");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, CompanyUpdateModel model)
    {
        try
        {
            await companyService.UpdateAsync(id, model);
            TempData["Success"] = "Company edited successfully!";
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            return RedirectToAction("Index");
        }
    }
}