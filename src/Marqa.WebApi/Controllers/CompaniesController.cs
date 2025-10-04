using Marqa.Service.Services.Companies;
using Marqa.Service.Services.Companies.Models;
using Microsoft.AspNetCore.Mvc;

namespace Marqa.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CompaniesController(ICompanyService companyService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> PostAsync(CompanyCreateModel model)
    {
        try
        {
            await companyService.CreateAsync(model);

            return Created();
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] CompanyUpdateModel model)
    {

        try
        {
            await companyService.UpdateAsync(id, model);

            return Ok();
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        try
        {
            await companyService.DeleteAsync(id);

            return Ok();
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetAsync(int id)
    {
        try
        {
            var company = await companyService.GetAsync(id);

            return Ok(company);
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        try
        {
            var companies = await companyService.GetAllAsync();

            return Ok(companies);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}