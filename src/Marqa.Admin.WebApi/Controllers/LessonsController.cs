using Marqa.Domain.Enums;
using Marqa.Service.Services.Lessons.Models;
using Microsoft.AspNetCore.Mvc;
using Marqa.Service.Services.Lessons;
using Marqa.Shared.Models;

namespace Marqa.Admin.WebApi.Controllers;

public class LessonsController(ILessonService lessonService) : BaseController
{
    [HttpPut("update/{id:int}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] LessonUpdateModel model)
    {
        await lessonService.UpdateAsync(id, model);

        return Ok(new Response
        {
            StatusCode = 200,
            Message = "success",
        });
    }
   
    [HttpPost("check-up")]
    public async Task<IActionResult> CheckUpAsync(LessonAttendanceModel model)
    {
        await lessonService.CheckUpAsync(model);

        return Ok(new Response
        {
            StatusCode = 200,
            Message = "success",
        });
    }

    [HttpPost("{id:int}/students")]
    public async Task<IActionResult> GetStudentsAsync(int id)
    {
        var result = await lessonService.GetCourseStudentsForCheckUpAsync(id);

        return Ok(new Response<List<StudentAttendanceModel>>
        {
            StatusCode = 200,
            Message = "success",
            Data = result
        });
    }
}
