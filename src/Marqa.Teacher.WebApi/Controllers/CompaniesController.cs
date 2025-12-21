﻿using Marqa.Service.Services.Companies;
using Marqa.Service.Services.Companies.Models;
using Marqa.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace Marqa.Teacher.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CompaniesController(ICompanyService companyService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> PostAsync(CompanyCreateModel model)
    {
        await companyService.CreateAsync(model);

        return Ok(new Response
        {
            StatusCode = 201,
            Message = "success",
        });
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] CompanyUpdateModel model)
    {
        await companyService.UpdateAsync(id, model);

        return Ok(new Response
        {
            StatusCode = 200,
            Message = "success"
        });
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await companyService.DeleteAsync(id);

        return Ok(new Response
        {
            StatusCode = 200,
            Message = "success"
        });
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetAsync(int id)
    {
        var company = await companyService.GetAsync(id);

        return Ok(new Response<CompanyViewModel>
        {
            StatusCode = 200,
            Message = "success",
            Data = company
        });
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var companies = await companyService.GetAllAsync();

        return Ok(new Response<List<CompanyViewModel>>
        {
            StatusCode = 200,
            Message = "success",
            Data = companies
        });
    }
}
