using Marqa.Service.Services.TeacherAssessments;
using Marqa.Service.Services.TeacherAssessments.Models;
using Microsoft.AspNetCore.Mvc;

namespace Marqa.Admin.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TeacherAssessmentsController : ControllerBase
{
    private readonly ITeacherAssessmentService assessmentService;

    public TeacherAssessmentsController(ITeacherAssessmentService assessmentService)
    {
        assessmentService = assessmentService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TeacherAssessmentCreateModel model)
    {
        await assessmentService.CreateAsync(model);

        return Ok(new
        {
            StatusCode = 200,
            Message = "Teacher assessment submitted successfully",
            SubmittedAt = DateTime.UtcNow
        });
    }
}