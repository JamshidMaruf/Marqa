using Marqa.Service.Exceptions;
using Marqa.Service.Services.Companies;
using Marqa.Service.Services.Companies.Models;
using Marqa.WebApi.Models;
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

            return Ok(new Response
            {
                Status = 201,
                Message = "success",
            });
        }
        catch(Exception ex)
        {
            return BadRequest(new Response
            {
                Status = 400,
                Message = ex.Message
            });
        }
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] CompanyUpdateModel model)
    {

        try
        {
            await companyService.UpdateAsync(id, model);

            return Ok(new Response
            {
                Status = 200,
                Message = "success"
            });
        }
        catch(NotFoundException ex)
        {
            return BadRequest(new Response
            {
                Status = ex.StatusCode,
                Message = ex.Message
            });
        }
        catch(Exception ex)
        {
            return BadRequest(new Response()
            {
                Status = 500,
                Message = ex.Message
            });
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

            return Ok(new Response<CompanyViewModel>
            {
                Status = 200,
                Message = "success",
                Data = company
            });
        }
        catch(NotFoundException ex)
        {
            return BadRequest(new Response
            {
                Status = ex.StatusCode,
                Message = ex.Message
            });
        }
        catch(Exception ex)
        {
            return BadRequest(new Response()
            {
                Status = 500,
                Message = ex.Message
            });
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        try
        {
            var companies = await companyService.GetAllAsync();

            return Ok(new Response<List<CompanyViewModel>>
            {
                Status = 200,
                Message = "success",
                Data = companies
            });
        }
        catch(NotFoundException ex)
        {
            return BadRequest(new Response
            {
                Status = ex.StatusCode,
                Message = ex.Message
            });
        }
        catch(Exception ex)
        {
            return BadRequest(new Response()
            {
                Status = 500,
                Message = ex.Message
            });
        }
    }
}