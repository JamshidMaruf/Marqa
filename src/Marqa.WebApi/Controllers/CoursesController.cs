using Marqa.Service.Services.Courses;
using Marqa.Service.Services.Courses.Models;
using Microsoft.AspNetCore.Mvc;

namespace Marqa.WebApi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class CoursesController(ICourseService courseService) : ControllerBase
{

    [HttpPost]
    public async Task<IActionResult> PostAsync(CourseCreateModel model)
    {
        try
        {
            await courseService.CreateAsync(model);
            return Created();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] CourseUpdateModel model)
    {
        try
        {
            await courseService.UpdateAsync(id, model);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        try
        {
            await courseService.DeleteAsync(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetAsync(int id)
    {
        try
        {
            var course = await courseService.GetAsync(id);
            return Ok(course);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] int companyId, [FromQuery] string search, [FromQuery] int? subjectId)
    {
        try
        {
            var courses = await courseService.GetAllAsync(companyId, search, subjectId);
            return Ok(courses);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
