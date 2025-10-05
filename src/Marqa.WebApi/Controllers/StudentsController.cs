using Marqa.Service.Services.Students;
using Marqa.Service.Services.Students.Models;
using Microsoft.AspNetCore.Mvc;

namespace Marqa.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController(IStudentService studentService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> PostAsync(StudentCreateModel model)
    {
        try
        {
            await studentService.CreateAsync(model);
            return Created();
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] StudentUpdateModel model)
    {
        try
        {
            await studentService.UpdateAsync(id, model);
            return Ok();
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        try
        {
            await studentService.DeleteAsync(id);
            return Ok();
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetAsync(int id)
    {
        try
        {
            var student = await studentService.GetAsync(id);
            return Ok(student);
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("course/{courseId:int}")]
    public async Task<IActionResult> GetAllAsync(int courseId)
    {
        try
        {
            var students = await studentService.GetAllByCourseAsync(courseId);
            return Ok(students);
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
