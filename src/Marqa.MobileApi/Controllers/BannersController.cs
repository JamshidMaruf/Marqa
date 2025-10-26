using Marqa.MobileApi.Models;
using Marqa.Service.Services.Banners.Models;
using Microsoft.AspNetCore.Mvc;

namespace Marqa.MobileApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BannersController(IBannerService bannerService) : ControllerBase
{
    [HttpGet("{companyId:int}")]
    public async Task<IActionResult> GetByCompanyIdAsync(int companyId)
    {
        var banners = await bannerService.GetByCompanyIdAsync(companyId);

        return Ok(new Response<List<BannerViewModel>>
        {
            StatusCode = 200,
            Message = "success",
            Data = banners
        });
    }
}