using Marqa.Domain.Enums;
using Marqa.Service.Services.Courses;
using Marqa.Service.Services.Courses.Models;
using Marqa.Service.Services.Students.Models;
using Marqa.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace Marqa.Admin.WebApi.Controllers;

public class CoursesController(ICourseService courseService) : BaseController
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

    
    [HttpGet("companies/{companyId:int}")]
    public async Task<IActionResult> GetAllAsync(
        int companyId,
        [FromQuery] PaginationParams @params,
        [FromQuery] string? search,  
        [FromQuery] CourseStatus status)
    {
        var courses = await courseService.GetAllAsync(@params, companyId, search, status);

        return Ok(new Response<List<CourseTableViewModel>>
        {
            StatusCode = 200,
            Message = "success",
            Data = courses
        });
    }
    
    
    [HttpGet("minimal-list/{companyId:int}")]
    public async Task<IActionResult> GetAllAsync(int companyId)
    {
        var courses = await courseService.GetMinimalListAsync(companyId);

        return Ok(new Response<List<CourseMinimalListModel>>
        {
            StatusCode = 200,
            Message = "success",
            Data = courses
        });
    }

    
    [HttpGet("upcomings/{courseId:int}/students")]
    public async Task<IActionResult> GetUpcomingCourseStudentsAsync(int courseId)
    {
        var course = await courseService.GetUpcomingCourseStudentsAsync(courseId);
        return Ok(new Response<UpcomingCourseViewModel>
        {
            StatusCode = 200,
            Message = "success",
            Data = course
        });
    }

    
    [HttpGet("list/{courseId:int}/students")]
    public async Task<IActionResult> GetStudentsListAsync(int courseId)
    {
        var students = await courseService.GetStudentsListAsync(courseId);
        return Ok(new Response<List<StudentList>>
        {
            StatusCode = 200,
            Message = "success", 
            Data = students
        });
    }

    
    [HttpPost("bulk-enroll-students")]
    public async Task<IActionResult> BulkEnrollStudentsAsync([FromBody] BulkEnrollStudentsModel model)
    {
        await courseService.BulkEnrollStudentsAsync(model);

        return Ok(new Response
        {
            StatusCode = 200,
            Message = "success"
        });
    }

    [HttpGet("{companyId:int}/statistics")]
    public async Task<IActionResult> GetStatisticsAsync(int companyId)
    {
        var result = await courseService.GetStatisticsAsync(companyId);

        return Ok(new Response<CoursesStatistics>
        {
            StatusCode = 200,
            Message = "success",
            Data = result
        });
    }
}
