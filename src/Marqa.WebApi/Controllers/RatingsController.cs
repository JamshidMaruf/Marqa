using Marqa.Service.Services.Ratings;
using Marqa.Service.Services.Ratings.Models;
using Marqa.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace Marqa.WebApi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class RatingsController(IRatingService ratingService) : Controller
{
    [HttpGet("{courseId:int}")]
    public async Task<IActionResult> GetStudentRatingsByCourseAsync(int courseId)
    {
        var ratings = await ratingService.GetStudentRatingsByCourseAsync(courseId);
        return Ok(new Response<List<Rating>>
        {
            Status = 200,
            Message = "success",
            Data = ratings.ToList()
        });
    }

    [HttpGet("MainPage/{companyId:int}")]
    public async Task<IActionResult> GetMainPageRatingResultAsync(int companyId)
    {
        var ratings = await ratingService.GetMainPageRatingResultAsync(companyId);
        return Ok(new Response<List<MainPageRatingResult>>
        {
            Status = 200,
            Message = "success",
            Data = ratings
        });
    }

    [HttpGet("All")]
    public async Task<IActionResult> GetAllStudentRatingsAsync()
    {
        var ratings = await ratingService.GetAllStudentRatingsAsync();
        return Ok(new Response<List<Rating>>
        {
            Status = 200,
            Message = "success",
            Data = ratings.ToList()
        });
    }

    [HttpGet("Student/{studentId:int}")]
    public async Task<IActionResult> GetStudentRatingAsync(int studentId)
    {
        var rating = await ratingService.GetStudentRatingAsync(studentId);
        return Ok(new Response<Rating>
        {
            Status = 200,
            Message = "success",
            Data = rating
        });
    }
}
