using Marqa.Service.Exceptions;
using Marqa.Service.Services.PointSettings;
using Marqa.Service.Services.PointSettings.Models;
using Marqa.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace Marqa.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PointSettingController(IPointSettingService pointSettingService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> PostAsync(PointSettingCreateModel model)
    {
        try
        {
            await pointSettingService.CreateAsync(model);

            return Ok(new Response
            {
                Status = 201,
                Message = "success",
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
    public async Task<IActionResult> PutAsync(int id, [FromBody] PointSettingUpdateModel model)
    {

        try
        {
            await pointSettingService.UpdateAsync(id, model);

            return Ok(new Response
            {
                Status = 200,
                Message = "success"
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
            return BadRequest(new Response()
            {
                Status = 500,
                Message = ex.Message
            });
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        try
        {
            await pointSettingService.DeleteAsync(id);

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
            var pointSetting = await pointSettingService.GetAsync(id);

            return Ok(new Response<PointSettingViewModel>
            {
                Status = 200,
                Message = "success",
                Data = pointSetting
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
            return BadRequest(new Response()
            {
                Status = 500,
                Message = ex.Message
            });
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync(string search)
    {
        try
        {
            var pointSettings = await pointSettingService.GetAllAsync(search);

            return Ok(new Response<IEnumerable<PointSettingViewModel>>
            {
                Status = 200,
                Message = "success",
                Data = pointSettings
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
            return BadRequest(new Response()
            {
                Status = 500,
                Message = ex.Message
            });
        }
    }

    [HttpPost("generate-token")]
    public IActionResult GenerateToken(TokenModel model)
    {
        try
        {
            string pointSettings = pointSettingService.GenerateToken(model);

            return Ok(new Response<string>
            {
                Status = 200,
                Message = "success",
                Data = pointSettings
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
            return BadRequest(new Response()
            {
                Status = 500,
                Message = ex.Message
            });
        }
    }
}
