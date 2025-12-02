using Marqa.Domain.Enums;
using Marqa.Service.Services.Enums;
using Marqa.Service.Services.Enums.Models;
using Marqa.Shared.Models;
using Microsoft.AspNetCore.Mvc;
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
}
