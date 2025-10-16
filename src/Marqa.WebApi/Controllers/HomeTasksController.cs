using Marqa.Service.Services.HomeTasks;
using Marqa.Service.Services.HomeTasks.Models;
using Marqa.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace Marqa.WebApi.Controllers;

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
            Status = 200,
            Message = "success",
        });
    }

    [HttpPut("update/{id:int}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] HomeTaskUpdateModel model)
    {
        await homeTaskService.UpdateAsync(id, model);

        return Ok(new Response
        {
            Status = 200,
            Message = "success",
        });
    }

    [HttpDelete("delete/{id:int}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await homeTaskService.DeleteAsync(id);

        return Ok(new Response
        {
            Status = 200,
            Message = "success",
        });
    }

    [HttpGet("get/{id:int}")]
    public async Task<IActionResult> GetAsync(int id)
    {
        var homeTask = await homeTaskService.GetAsync(id);

        return Ok(new Response<List<HomeTaskViewModel>>
        {
            Status = 200,
            Message = "success",
            Data = homeTask,
        });
    }
}
