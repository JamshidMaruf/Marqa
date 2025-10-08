using Marqa.Service.Exceptions;
using Marqa.Service.Services.Lessons;
using Marqa.Service.Services.Lessons.Models;
using Marqa.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace Marqa.WebApi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class LessonsController(ILessonService lessonService) : Controller
{
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] LessonUpdateModel model)
    {
        try
        {
            await lessonService.UpdateAsync(id, model);
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
            return NotFound(new Response
            {
                Status = 404,
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

    [HttpPatch]
    public async Task<IActionResult> PatchAsync(int Id, string name)
    {
        try
        {
            await lessonService.ModifyAsync(Id, name);

            return Ok(new Response
            {
                Status = 200,
                Message = "success",
            });
        }
        catch (NotFoundException ex)
        {
            return NotFound(new Response
            {
                Status = 404,
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
    [HttpPost("CheckUp")]
    public async Task<IActionResult> CheckUpAsync(LessonAttendanceModel model)
    {
        try
        {
            await lessonService.CheckUpAsync(model);
            return Ok(new Response
            {
                Status = 200,
                Message = "success",
            });
        }
        catch (NotFoundException ex)
        {
            return NotFound(new Response
            {
                Status = 404,
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
