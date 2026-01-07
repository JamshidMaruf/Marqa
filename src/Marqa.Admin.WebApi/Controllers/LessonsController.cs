using Marqa.Service.Services.Lessons;
using Marqa.Service.Services.Lessons.LessonSchedules.Models;
using Marqa.Service.Services.Lessons.Models;
using Marqa.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace Marqa.Admin.WebApi.Controllers;

public class LessonsController(ILessonService lessonService, ILessonSchedule lessonSchedule) : BaseController
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

    [HttpGet("{id:int}/students")]
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

    [HttpGet("{companyId:int}/statistics")]
    public async Task<IActionResult> GetStatisticsAsync(int companyId)
    {
        var result = await lessonService.GetStatisticsAsync(companyId);

        return Ok(new Response<CurrentAttendanceStatistics>
        {
            StatusCode = 200,
            Message = "success",
            Data = result
        });
    }

    [HttpGet]
    public async Task<IActionResult> GetLessonsAsync([FromQuery] DateOnly date)
    {
        var result = await lessonService.GetCoursesLessonsAsync(date);

        return Ok(new Response<List<CourseLesson>>
        {
            StatusCode = 200,
            Message = "success",
            Data = result
        });
    }

    //[HttpGet("{companyId:int}/schedule")]
    //public async Task<IActionResult> GetScheduleAsync(int companyId)
    //{
    //    var schedule = await lessonSchedule.GetWeekLessonScheduleAsync(companyId);
    //    return Ok(new Response<WeekLessonScheduleModel>
    //    {
    //        StatusCode = 200,
    //        Message = "success",
    //        Data = schedule
    //    });
    //}
}
