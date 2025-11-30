using Marqa.Service.Services.Enrollments;
using Marqa.Service.Services.Enrollments.Models;
using Microsoft.AspNetCore.Mvc;

namespace Marqa.Admin.WebApi.Controllers;

public class EnrollmentsController(IEnrollmentService enrollmentService) : BaseController
{
    [HttpPost("attach")]
    public IActionResult AttachAsync(EnrollmentCreateModel model)
    {
        // logic
        return Ok();
    }

    [HttpPost("detach")]
    public IActionResult DetachAsync(DetachModel model)
    {
        // logic
        return Ok();
    }

    [HttpPost("freeze")]
    public IActionResult Freeze(FreezeModel model)
    {
        return Ok();
    }

    [HttpGet("frozen-courses")]
    public IActionResult GetFrozenCoursesAsync(int studentId)
    {
        return Ok();
    }

    [HttpPost("unfreeze")]
    public IActionResult Unfreeze(UnFreezeModel model)
    {
        return Ok();
    }
}

