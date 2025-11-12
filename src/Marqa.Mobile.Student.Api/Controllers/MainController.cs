using Marqa.Mobile.Student.Api.Models;
using Marqa.Service.Services.Banners.Models;
using Marqa.Service.Services.Courses;
using Marqa.Service.Services.Courses.Models;
using Marqa.Service.Services.Ratings;
using Marqa.Service.Services.Ratings.Models;
using Marqa.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace Marqa.Mobile.Student.Api.Controllers;

public class MainController(ICourseService courseService, IBannerService bannerService, IRatingService ratingService) : BaseController
{
    [HttpGet("student/{studentId:int}/courses")]
    public async Task<IActionResult> GetStudentCoursesAsync(int studentId)
    {
        return Ok(new Response<List<MainPageCourseViewModel>>
        {
            StatusCode = 200,
            Message = "success",
            Data = await courseService.GetCoursesByStudentIdAsync(studentId)
        });
    }

    [HttpGet("banners/{companyId:int}")]
    public async Task<IActionResult> GetBannersAsync(int companyId)
    {
        return Ok(new Response<List<MainPageBannerViewModel>>
        {
            StatusCode = 200,
            Message = "success",
            Data = await bannerService.GetByCompanyIdAsync(companyId)
        });
    }

    [HttpGet("rating/{companyId:int}")]
    public async Task<IActionResult> GetRatingAsync(int companyId)
    {
        return Ok(new Response<List<MainPageRatingResult>>
        {
            StatusCode = 200,
            Message = "success",
            Data = await ratingService.GetMainPageRatingResultAsync(companyId)
        });
    }
}
