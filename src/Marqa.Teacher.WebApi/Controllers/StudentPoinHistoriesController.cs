using Marqa.Service.Services.StudentPointHistories;
using Marqa.Service.Services.StudentPointHistories.Models;
using Marqa.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace Marqa.Teacher.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentPoinHistoriesController(IStudentPointHistoryService studentPointHistoryService) : ControllerBase
{
    [HttpPost("point/add")]
    public async Task<IActionResult> PostAsync(StudentPointAddModel model)
    {
        await studentPointHistoryService.AddPointAsync(model);

        return Ok(new Response
        {
            StatusCode = 200,
            Message = "success"
        });
    }

    [HttpGet("summPoint/{studentId:int}")]
    public async Task<IActionResult> GetAsync(int studentId)
    {
        var point = await studentPointHistoryService.GetAsync(studentId);

        return Ok(new Response<int>
        {
            StatusCode = 200,
            Message = "success",
            Data = point,
        });
    }

    [HttpGet("getAllPointHistories/{studentId:int}")]
    public async Task<IActionResult> GetAll(int studentId)
    {
        var pointHistories = await studentPointHistoryService.GetAllAsync(studentId);

        return Ok(new Response<List<StudentPointHistoryViewModel>>
        {
            StatusCode = 200,
            Message = "success",
            Data = pointHistories,
        });
    }
}
