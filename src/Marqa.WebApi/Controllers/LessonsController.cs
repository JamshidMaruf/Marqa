using Marqa.Domain.Enums;
using Marqa.Service.Services.Lessons;
using Marqa.Service.Services.Lessons.Models;
using Marqa.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace Marqa.WebApi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class LessonsController(ILessonService lessonService) : Controller
{
    [HttpPut("update/{id:int}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] LessonUpdateModel model)
    {
        await lessonService.UpdateAsync(id, model);

        return Ok(new Response
        {
            Status = 200,
            Message = "success",
        });
    }

    [HttpPatch("{name:required}")]
    public async Task<IActionResult> PatchAsync(int id, string name, HomeTaskStatus status)
    {

        await lessonService.ModifyAsync(id, name, status);

        return Ok(new Response
        {
            Status = 200,
            Message = "success",
        });
    }

    [HttpPost("check-up")]
    public async Task<IActionResult> CheckUpAsync(LessonAttendanceModel model)
    {
        await lessonService.CheckUpAsync(model);

        return Ok(new Response
        {
            Status = 200,
            Message = "success",
        });
    }
}
