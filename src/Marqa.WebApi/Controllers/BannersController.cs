using Marqa.Service.Services.Banners.Models;
using Microsoft.AspNetCore.Mvc;

namespace Marqa.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BannersController(IBannerService bannerService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateAsync(BannerCreateModel model)
    {
        await bannerService.CreateAsync(model);
        return Ok("Created");
    }
}