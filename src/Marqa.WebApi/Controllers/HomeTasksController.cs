using Marqa.Service.Services.HomeTasks;
using Marqa.Service.Services.HomeTasks.Models;
using Microsoft.AspNetCore.Mvc;

namespace Marqa.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HomeTasksController(IHomeTaskService homeTaskService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> PostAsync(HomeTaskCreateModel model)
    {
        try
        {
            await homeTaskService.CreateAsync(model);

            return Created();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] HomeTaskUpdateModel model)
    {
        try
        {
            await homeTaskService.UpdateAsync(id, model);

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
            await homeTaskService.DeleteAsync(id);

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
            var homeTask = await homeTaskService.GetAsync(id);

            return Ok(homeTask);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
