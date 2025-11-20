using Marqa.Service.Services.PointSettings;
using Marqa.Service.Services.PointSettings.Models;
using Marqa.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace Marqa.Teacher.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PointSettingsController(IPointSettingService pointSettingService) : ControllerBase
{
    [HttpPost("create")]
    public async Task<IActionResult> PostAsync(PointSettingCreateModel model)
    {
        await pointSettingService.CreateAsync(model);

        return Ok(new Response
        {
            StatusCode = 201,
            Message = "success",
        });
    }

    [HttpPut("update/{id:int}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] PointSettingUpdateModel model)
    {
        await pointSettingService.UpdateAsync(id, model);

        return Ok(new Response
        {
            StatusCode = 200,
            Message = "success"
        });
    }

    [HttpDelete("delete/{id:int}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await pointSettingService.DeleteAsync(id);

        return Ok();
    }

    [HttpGet("get/{id:int}")]
    public async Task<IActionResult> GetAsync(int id)
    {
        var pointSetting = await pointSettingService.GetAsync(id);

        return Ok(new Response<PointSettingViewModel>
        {
            StatusCode = 200,
            Message = "success",
            Data = pointSetting
        });
    }

    [HttpGet("getAll")]
    public async Task<IActionResult> GetAllAsync(string search)
    {
        var pointSettings = await pointSettingService.GetAllAsync(search);

        return Ok(new Response<IEnumerable<PointSettingViewModel>>
        {
            StatusCode = 200,
            Message = "success",
            Data = pointSettings
        });
    }

    [HttpPost("generate-token")]
    public IActionResult GenerateToken(TokenModel model)
    {
        string pointSettings = pointSettingService.GenerateToken(model);

        return Ok(new Response<string>
        {
            StatusCode = 200,
            Message = "success",
            Data = pointSettings
        });
    }

    [HttpPost("decode-token")]
    public IActionResult DecodeToken(string token)
    {
        TokenModel tokenModel = pointSettingService.DecodeToken(token);

        return Ok(new Response<TokenModel>
        {
            StatusCode = 200,
            Message = "success",
            Data = tokenModel
        });
    }
}
