using Marqa.Service.Services.HomeTasks;
using Marqa.Service.Services.HomeTasks.Models;
using Marqa.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace Marqa.Teacher.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HomeTasksController(IHomeTaskService homeTaskService) : ControllerBase
{
    [HttpPost("create")]
    public async Task<IActionResult> PostAsync(HomeTaskCreateModel model)
    {
        await homeTaskService.CreateAsync(model);

        return Ok(new Response
        {
            StatusCode = 200,
            Message = "success",
        });
    }

    [HttpPut("update/{id:int}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] HomeTaskUpdateModel model)
    {
        await homeTaskService.UpdateAsync(id, model);

        return Ok(new Response
        {
            StatusCode = 200,
            Message = "success",
        });
    }

    [HttpDelete("delete/{id:int}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await homeTaskService.DeleteAsync(id);

        return Ok(new Response
        {
            StatusCode = 200,
            Message = "success",
        });
    }

    [HttpGet("get/{id:int}")]
    public async Task<IActionResult> GetAsync(int id)
    {
        var homeTask = await homeTaskService.GetAsync(id);

        return Ok(new Response<List<HomeTaskViewModel>>
        {
            StatusCode = 200,
            Message = "success",
            Data = homeTask,
        });
    }

    [HttpPost("file-upload")]
    public async Task<IActionResult> UploadHomeTaskFileAsync(int homeTaskId, IFormFile file)
    {
        await homeTaskService.UploadHomeTaskFileAsync(homeTaskId, file);

        return Ok(new Response
        {
            StatusCode = 200,
            Message = "success",
        });
    }

    [HttpPost("student-home-task")]
    public async Task<IActionResult> StudentHomeTaskCreateAsync(StudentHomeTaskCreateModel model)
    {
        await homeTaskService.StudentHomeTaskCreateAsync(model);

        return Ok(new Response
        {
            StatusCode = 200,
            Message = "success",
        });
    }

    [HttpPost("student-file-upload")]
    public async Task<IActionResult> StudentHomeTaskFileUploadAsync(int homeTaskId, IFormFile file)
    {
        await homeTaskService.UploadStudentHomeTaskFileAsync(homeTaskId, file);

        return Ok(new Response
        {
            StatusCode = 200,
            Message = "success",
        });
    }

    [HttpPut("student-hometask-assessment")]
    public async Task<IActionResult> StudentHomeTaskAssessmentAsync(HomeTaskAssessmentModel model)
    {
        await homeTaskService.HomeTaskAssessmentAsync(model);

        return Ok(new Response
        {
            StatusCode = 200,
            Message = "success",
        });
    }
}
