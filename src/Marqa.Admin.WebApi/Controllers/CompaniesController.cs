using Marqa.Service.Services.Companies;
using Marqa.Service.Services.Companies.Models;
using Marqa.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace Marqa.Admin.WebApi.Controllers;

public class CompaniesController(ICompanyService companyService) : BaseController
{
    [HttpPost]
    public async Task<IActionResult> CreateAsync(CompanyCreateModel model)
    {
        await companyService.CreateAsync(model);
        return Ok("Created");
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] PaginationParams @params)
    {
        var result = await companyService.GetAllAsync(@params);
        return Ok(result);
    }
}