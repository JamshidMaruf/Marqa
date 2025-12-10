using Marqa.Service.Exceptions;
using Marqa.Service.Services.Teachers;
using Marqa.Service.Services.Teachers.Models;
using Marqa.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace Marqa.Admin.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TeachersController(ITeacherService teacherService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> PostAsync(TeacherCreateModel model)
    {
        await teacherService.CreateAsync(model);

        return Ok(new Response
        {
            StatusCode = 201,
            Message = "Teacher created successfully"
        });
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] TeacherUpdateModel model)
    {
        await teacherService.UpdateAsync(id, model);

        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Teacher updated successfully"
        });
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        try
        {
            await teacherService.DeleteAsync(id);

            return Ok(new Response
            {
                StatusCode = 200,
                Message = "Teacher deleted successfully"
            });
        }
        catch (NotFoundException ex)
        {
            return NotFound(new Response
            {
                StatusCode = 404,
                Message = ex.Message
            });
        }
        catch (CannotDeleteException ex) 
        {
            return BadRequest(new Response
            {
                StatusCode = 400,
                Message = ex.Message
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new Response
            {
                StatusCode = 500,
                Message = "Internal server error"
            });
        }
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetAsync(int id)
    {
        var result = await teacherService.GetAsync(id);

        return Ok(new Response<TeacherViewModel>
        {
            StatusCode = 200,
            Message = "success",
            Data = result
        });
    }

    [HttpGet("{id:int}/update")]
    public async Task<IActionResult> GetForUpdateAsync(int id)
    {
        var result = await teacherService.GetForUpdateAsync(id);

        return Ok(new Response<TeacherUpdateViewModel>
        {
            StatusCode = 200,
            Message = "success",
            Data = result
        });
    }

    [HttpGet("company/{companyId:int}")]
    public async Task<IActionResult> GetAllTeachersAsync(int companyId, [FromQuery] string? search, [FromQuery] int? subjectId)
    {
        var result = await teacherService.GetAllAsync(companyId, search, subjectId);

        return Ok(new Response<IEnumerable<TeacherTableViewModel>>
        {
            StatusCode = 200,
            Message = "success",
            Data = result
        });
    }
}

