using Marqa.Service.Services.StudentPointHistories;
using Marqa.Service.Services.StudentPointHistories.Models;
using Microsoft.AspNetCore.Mvc;

namespace Marqa.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentPoinHistoriesController(IStudentPointHistoryService studentPointHistoryService) : ControllerBase
{
    [HttpPost("point/add")]
    public Task<IActionResult> PostAsync(StudentPointAddModel model)
    {
        try
        {
            
        }
        catch
        {

        }
    }
}
