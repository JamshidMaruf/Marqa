using Marqa.MobileApi.Models;
using Marqa.Service.Services.Companies;
using Marqa.Service.Services.Companies.Models;
using Microsoft.AspNetCore.Mvc;

namespace Marqa.MobileApi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class CompaniesController(ICompanyService companyService) : ControllerBase
{
    [HttpGet("{studentId:int}")]
    public async Task<IActionResult> GetByStudentIdAsync(int studentId)
    {
        var companies = await companyService.GetAsync(studentId);

        return Ok(new Response<List<CompanyViewModel>>
        {
            StatusCode = 200,
            Message = "success",
            Data = companies
        });
    }
}

