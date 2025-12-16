using System.Threading.Tasks;
using Marqa.Service.Services.Companies;
using Marqa.Service.Services.Companies.Models;
using Microsoft.AspNetCore.Mvc;

namespace Marqa.Admin.Controllers;

public class CompaniesController(ICompanyService companyService) : Controller
{
    public async Task<IActionResult> Index(string search = null)
    {
        try
        {
            ViewBag.Search = search;

            var companies = await companyService.GetAllAsync(search);

            return View(companies);
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
            var companyDataToUpdate = await companyService.GetAsync(id);
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
        //
    }
}