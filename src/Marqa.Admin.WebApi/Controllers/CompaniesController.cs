using Marqa.Service.Services.Companies;
using Marqa.Service.Services.Companies.Models;
using Marqa.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace Marqa.Admin.WebApi.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CompaniesController(ICompanyService companyService) : ControllerBase
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