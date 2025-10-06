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
        try
        {
            await courseService.CreateAsync(model);
            return Ok(new Response
            {
                Status = 200,
                Message = "success",
            });
        }
        catch(AlreadyExistException ex)
        {
            return BadRequest(new Response
            {
                Status = ex.StatusCode,
                Message = ex.Message,
            });
        }
        catch (NotFoundException ex)
        {
            return BadRequest(new Response
            {
                Status = ex.StatusCode,
                Message = ex.Message
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new Response
            {
                Status = 400,
                Message = ex.Message
            });
        }
    }
    [HttpPost("AttachStudent")]
    public async Task<IActionResult> AttachStudentAsync([FromQuery] int courseId, [FromQuery] int studentId)
    {
        try
        {
            await courseService.AttachStudentAsync(courseId, studentId);
            return Ok(new Response
            {
                Status = 200,
                Message = "success",
            });
        }
        catch(NotFoundException ex)
        {
            return BadRequest(new Response
            {
                Status = ex.StatusCode,
                Message = ex.Message
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new Response
            {
                Status = 400,
                Message = ex.Message
            });
        }
      
    }
    
    [HttpPost("DetachStudent")]
    public async Task<IActionResult> DetachStudentAsync([FromQuery] int courseId, [FromQuery] int studentId)
    {
        try
        {
            await courseService.DetachStudentAsync(courseId, studentId);
            return Ok(new Response
            {
                Status = 200,
                Message = "success",
            });
        }
        catch (NotFoundException ex)
        {
            return BadRequest(new Response
            {
                Status = ex.StatusCode,
                Message = ex.Message
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new Response
            {
                Status = 400,
                Message = ex.Message
            });
        }
    }
    
    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] CourseUpdateModel model)
    {
        try
        {
            await courseService.UpdateAsync(id, model);
            return Ok(new Response
            {
                Status = 200,
                Message = "success",
            });
        }
        catch(NotFoundException ex)
        {
            return BadRequest(new Response
            {
                Status = ex.StatusCode,
                Message = ex.Message
            });
        }
        catch(AlreadyExistException ex)
        {
            return BadRequest(new Response
            {
                Status = ex.StatusCode,
                Message = ex.Message,
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new Response
            {
                Status = 400,
                Message = ex.Message
            });
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        try
        {
            await courseService.DeleteAsync(id);
            return Ok(new Response
            {
                Status = 200,
                Message = "success",
            });
        }
        catch(NotFoundException ex)
        {
            return BadRequest(new Response
            {
                Status = ex.StatusCode,
                Message = ex.Message
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new Response
            {
                Status = 400,
                Message = ex.Message
            });
        }
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetAsync(int id)
    {
        try
        {
            var course = await courseService.GetAsync(id);
            return Ok(new Response<CourseViewModel>
            {
                Status = 200,
                Message = "success",
                Data = course
            });
        }
        catch(NotFoundException ex)
        {
            return BadRequest(new Response
            {
                Status = ex.StatusCode,
                Message = ex.Message
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new Response
            {
                Status = 400,
                Message = ex.Message
            });
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] int companyId, [FromQuery] string search, [FromQuery] int? subjectId)
    {
        try
        {
            var courses = await courseService.GetAllAsync(companyId, search, subjectId);
            return Ok(new Response<List<CourseViewModel>>
            {
                Status = 200,
                Message = "success",
                Data = courses
            });
        }
        catch (NotFoundException ex)
        {
            return BadRequest(new Response
            {
                Status = ex.StatusCode,
                Message = ex.Message
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new Response
            {
                Status = 400,
                Message = ex.Message
            });
        }
    }
}
