using Marqa.Mobile.Student.Api.Models;
using Marqa.Service.Services.Ratings;
using Marqa.Service.Services.Ratings.Models;
using Microsoft.AspNetCore.Mvc;

namespace Marqa.Mobile.Student.Api.Controllers;

public class RatingController(IRatingService ratingService) : BaseController
{
    [HttpGet("rating/{companyId:int}")]
    public async Task<IActionResult> GetRatingAsync(int companyId)
    {
        return Ok(new Response<List<RatingPageRatingResult>>
        {
            StatusCode = 200,
            Message = "success",
            Data = await ratingService.GetRatingPageRatingResultAsync(companyId)
        });
    }
}
