using Marqa.Service.Services.Banners.Models;
using Microsoft.AspNetCore.Mvc;

namespace Marqa.Admin.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BannersController(IBannerService bannerService) : BaseController
{
    [HttpPost]
    public async Task<IActionResult> CreateAsync(BannerCreateModel model)
    {
        await bannerService.CreateAsync(model);
        return Ok("Created");
    }
}