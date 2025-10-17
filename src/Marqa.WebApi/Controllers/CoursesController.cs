using Marqa.Service.Exceptions;
using Marqa.Service.Services.Courses;
using Marqa.Service.Services.Courses.Models;
using Marqa.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace Marqa.WebApi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class CoursesController(ICourseService courseService) : ControllerBase
{

    [HttpPost]
    public async Task<IActionResult> PostAsync(CourseCreateModel model)
    {
        await courseService.CreateAsync(model);
        return Ok(new Response
        {
            Status = 200,
            Message = "success",
        });
    }

    [HttpPost("AttachStudent")]
    public async Task<IActionResult> AttachStudentAsync([FromQuery] int courseId, [FromQuery] int studentId)
    {
        await courseService.AttachStudentAsync(courseId, studentId);
        return Ok(new Response
        {
            Status = 200,
            Message = "success",
        });
    }

    [HttpPost("DetachStudent")]
    public async Task<IActionResult> DetachStudentAsync([FromQuery] int courseId, [FromQuery] int studentId)
    {
        await courseService.DetachStudentAsync(courseId, studentId);

        return Ok(new Response
        {
            Status = 200,
            Message = "success",
        });
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] CourseUpdateModel model)
    {
        await courseService.UpdateAsync(id, model);

        return Ok(new Response
        {
            Status = 200,
            Message = "success",
        });
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await courseService.DeleteAsync(id);

        return Ok(new Response
        {
            Status = 200,
            Message = "success",
        });
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetAsync(int id)
    {
        var course = await courseService.GetAsync(id);

        return Ok(new Response<CourseViewModel>
        {
            Status = 200,
            Message = "success",
            Data = course
        });
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] int companyId, [FromQuery] string search, [FromQuery] int? subjectId)
    {
        var courses = await courseService.GetAllAsync(companyId, search, subjectId);

        return Ok(new Response<List<CourseViewModel>>
        {
            Status = 200,
            Message = "success",
            Data = courses
        });

    }
}
