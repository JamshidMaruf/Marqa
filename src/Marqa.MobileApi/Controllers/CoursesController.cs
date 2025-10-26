using Marqa.MobileApi.Models;
using Marqa.Service.Services.Courses;
using Marqa.Service.Services.Courses.Models;
using Marqa.Service.Services.Lessons.Models;
using Microsoft.AspNetCore.Mvc;

namespace Marqa.MobileApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CoursesController(ICourseService courseService) : ControllerBase
{
    // 5. api/courses/home-page/{studentId}
    [HttpGet("home-page/{studentId:int}")]
    public async Task<IActionResult> GetHomePageAsync(int studentId)
    {
        var homePage = await courseService.GetAsync(studentId);

        return Ok(new Response<CourseHomePageViewModel>
        {
            StatusCode = 200,
            Message = "success",
            Data = homePage
        });
    }

    // 8. api/courses/{studentId}
    [HttpGet("{studentId:int}")]
    public async Task<IActionResult> GetByStudentIdAsync(int id)
    {
        var courses = await courseService.GetAsync(id);

        return Ok(new Response<List<CourseViewModel>>
        {
            StatusCode = 200,
            Message = "success",
            Data = courses
        });
    }

    // 9. api/courses/{courseId}/lessons
    [HttpGet("{courseId:int}/lessons")]
    public async Task<IActionResult> GetLessonsAsync(int courseId)
    {
        var lessons = await courseService.GetLessonsByCourseIdAsync(courseId);

        return Ok(new Response<List<LessonViewModel>>
        {
            StatusCode = 200,
            Message = "success",
            Data = lessons
        });
    }
}
