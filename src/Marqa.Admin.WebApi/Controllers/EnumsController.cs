using Marqa.Domain.Enums;
using Marqa.Service.Services.Enums;
using Marqa.Service.Services.Enums.Models;
using Marqa.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Extensions;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace Marqa.Admin.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EnumsController : ControllerBase
{
    private readonly IEnumService _enumService;

    public EnumsController(IEnumService enumService)
    {
        _enumService = enumService;
    }

    [HttpGet("genders")]
    public async Task<IActionResult> GetGenders()
    {
        var result = _enumService.GetEnumValues<Gender>();
        return Ok(new Response<List<EnumGetModel>>
        {
            StatusCode = 200,
            Message = "success",
            Data = result
        });
    }

    [HttpGet("report-periods")]
    public async Task<IActionResult> GetReportPeriods()
    {
        var result = _enumService.GetEnumValues<ReportPeriod>();
        return Ok(new Response<List<EnumGetModel>>
        {
            StatusCode = 200,
            Message = "success",
            Data = result
        });
    }

    [HttpGet("payment-operation-types")]
    public async Task<IActionResult> GetPaymentOperationTypes()
    {
        var result = _enumService.GetEnumValues<PaymentOperationType>();
        return Ok(new Response<List<EnumGetModel>>
        {
            StatusCode = 200,
            Message = "success",
            Data = result
        });
    }

    [HttpGet("employee-statuses")]
    public async Task<IActionResult> GetEmployeeStatuses()
    {
        var result = _enumService.GetEnumValues<EmployeeStatus>();
        return Ok(new Response<List<EnumGetModel>>
        {
            StatusCode = 200,
            Message = "success",
            Data = result
        });
    }
    
    [HttpGet("enrollment-status")]
    public async Task<IActionResult> GetEnrollmentStatusAsync()
    {
        var r = EnrollmentStatus.Active;
        r.GetDisplayName();
        var result = _enumService.GetEnumValues<EnrollmentStatus>();
        return Ok(new Response<List<EnumGetModel>>
        {
            StatusCode = 200,
            Message = "success",
            Data = result
        });
    }
    [HttpGet("course-payment-type")]
    public async Task<IActionResult> GetCoursePaymentTypeAsync()
    {
        var result = _enumService.GetEnumValues<CoursePaymentType>();
        return Ok(new Response<List<EnumGetModel>>
        {
            StatusCode = 200,
            Message = "success",
            Data = result
        });
    }
    [HttpGet("employee-payment-operation-type")]
    public async Task<IActionResult> GetEmployeePaymentOperationType()
    {
        var result = _enumService.GetEnumValues<EmployeePaymentOperationType>();
        return Ok(new Response<List<EnumGetModel>>
        {
            StatusCode = 200,
            Message = "success",
            Data = result
        });
    }
    [HttpGet("payment-method")]
    public async Task<IActionResult> GetPaymentMethod()
    {
        var result = _enumService.GetEnumValues<PaymentMethod>();
        return Ok(new Response<List<EnumGetModel>>
        {
            StatusCode = 200,
            Message = "success",
            Data = result
        });
    }
    [HttpGet("student-status")]
    public async Task<IActionResult> GetStudentStatus()
    {
        var result = _enumService.GetEnumValues<StudentStatus>();
        return Ok(new Response<List<EnumGetModel>>
        {
            StatusCode = 200,
            Message = "success",
            Data = result
        });
    }

    [HttpGet("current-yearly-months")]
    public async Task<IActionResult> GetYearlyMonths()
    {
        var result = _enumService.GetCurrentYearlyMonths();

        return Ok(new Response<YearlyMonths>
        {
            StatusCode = 200,
            Message = "success",
            Data = result
        });
    }
}
