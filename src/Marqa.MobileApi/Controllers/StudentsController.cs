using Marqa.MobileApi.Models;
using Marqa.Service.Services.Students;
using Microsoft.AspNetCore.Mvc;

namespace Marqa.MobileApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController(IStudentService studentService) : ControllerBase
{
    // ... mavjud metodlar (create, update, delete, get, courseStudents)

    // 4. api/students/{studentId}/points
    [HttpGet("{studentId:int}/points")]
    public async Task<IActionResult> GetPointsAsync(int studentId)
    {
        var points = await studentService.GetPointsAsync(studentId);

        return Ok(new Response<StudentPointsViewModel>
        {
            Status = 200,
            Message = "success",
            Data = points
        });
    }

    // 7. api/students/rating/{companyId}
    [HttpPost("rating/{companyId:int}")]
    public async Task<IActionResult> RateCompanyAsync(int companyId, [FromBody] StudentRatingModel model)
    {
        await studentService.RateCompanyAsync(companyId, model);

        return Ok(new Response
        {
            Status = 200,
            Message = "success"
        });
    }
}

// ============================================
// Controllers/CoursesController.cs
// ============================================
using Marqa.Service.Services.Courses;
using Marqa.Service.Services.Courses.Models;
using Marqa.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
