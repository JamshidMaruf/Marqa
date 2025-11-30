using Marqa.Admin.WebApi.Filters;
using Marqa.Domain.Enums;
using Marqa.Service.Services.Courses;
using Marqa.Service.Services.Courses.Models;
using Marqa.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Marqa.Admin.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CoursesController(ICourseService courseService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] CourseCreateModel model)
    {
        await courseService.CreateAsync(model);

        return Ok(new Response
        {
            StatusCode = 201,
            Message = "success"
        });
    }

    [RequirePermission("students.attach")]
    [HttpPost("attach-student")]
    public async Task<IActionResult> AttachStudentAsync(int courseId, int studentId, StudentStatus status)
    {
        await courseService.AttachStudentAsync(courseId, studentId, status);

        return Ok(new Response
        {
            StatusCode = 200,
            Message = "success"
        });
    }

    [HttpPost("detach-student")]
    public async Task<IActionResult> DetachStudentAsync(int courseId, int studentId)
    {
        await courseService.DetachStudentAsync(courseId, studentId);

        return Ok(new Response
        {
            StatusCode = 200,
            Message = "success"
        });
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] CourseUpdateModel model)
    {
        await courseService.UpdateAsync(id, model);

        return Ok(new Response
        {
            StatusCode = 200,
            Message = "success"
        });
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await courseService.DeleteAsync(id);

        return Ok(new Response
        {
            StatusCode = 200,
            Message = "success"
        });
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetAsync(int id)
    {
        var course = await courseService.GetAsync(id);

        return Ok(new Response<CourseViewModel>
        {
            StatusCode = 200,
            Message = "success",
            Data = course
        });
    }

    [HttpGet("{id:int}/update")]
    public async Task<IActionResult> GetByUpdateAsync(int id)
    {
        var course = await courseService.GetForUpdateAsync(id);

        return Ok(new Response<CourseUpdateViewModel>
        {
            StatusCode = 200,
            Message = "success",
            Data = course
        });
    }

    [HttpGet("companies/{companyId:int}/courses")]
    public async Task<IActionResult> GetAllAsync(int companyId, [FromQuery] string? search, [FromQuery] int? subjectId)
    {
        var courses = await courseService.GetAllAsync(companyId, search, subjectId);

        return Ok(new Response<List<CourseViewModel>>
        {
            StatusCode = 200,
            Message = "success",
            Data = courses
        });
    }
}
