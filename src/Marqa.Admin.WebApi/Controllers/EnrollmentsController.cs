using Microsoft.AspNetCore.Mvc;

namespace Marqa.Admin.WebApi.Controllers;

public class EnrollmentsController : BaseController
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

public class EnrollmentCreateModel
{
    public int StudentId { get; set; }
    public int CourseId { get; set; }   
    public DateTime EnrolledDate { get; set; }
    public int Status { get; set; }
    public CoursePaymentType PaymentType { get; set; }
    public decimal Amount { get; set; }
}

public class FreezeModel
{
    public int StudentId { get; set; }
    public List<int> CourseIds { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsInfinite { get; set; }
    public string Reason { get; set; }
}

public class UnFreezeModel
{
    public int StudentId { get; set; }
    public List<int> CourseIds { get; set; }
    public DateTime ActivateDate { get; set; }
}

public class DetachModel
{
    public int StudentId { get; set; }
    public List<int> CourseIds { get; set; }
    public string Reason { get; set; }
    public DateTime DeactivatedDate { get; set; }
}