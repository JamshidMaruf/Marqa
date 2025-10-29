using Marqa.MobileApi.Models;
using Marqa.Service.Services.Courses;
using Marqa.Service.Services.Courses.Models;
using Marqa.Service.Services.Lessons;
using Marqa.Service.Services.Lessons.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Marqa.MobileApi.Controllers;

public class CourseController(ICourseService courseService, ILessonService lessonService) : BaseController
{
    [HttpGet("{studentId:int}")]
    public async Task<IActionResult> GetByStudentIdAsync(int studentId)
    {
        return Ok(new Response<List<CoursePageCourseViewModel>>
        {
            StatusCode = 200,
            Message = "success",
            Data = await courseService.GetNameByStudentIdAsync(studentId)
        });
    }

    [HttpGet("{courseId:int}/lessons")]
    public async Task<IActionResult> GetLessonsByCourseIdAsync(int courseId)
    {
        return Ok(new Response<List<LessonViewModel>>
        {
            StatusCode = 200,
            Message = "success",
            Data = await lessonService.GetByCourseIdAsync(courseId)
        });
    }
}