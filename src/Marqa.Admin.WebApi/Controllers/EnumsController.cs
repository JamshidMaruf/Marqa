using Marqa.Domain.Enums;
using Marqa.Service.Services.Enum;
using Marqa.Service.Services.Enum.Models;
using Marqa.Shared.Models;
using Microsoft.AspNetCore.Components;
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
        return Ok(new ApiResponse<List<EnumGetModel>>(result));
    }

    [HttpGet("report-periods")]
    public async Task<IActionResult> GetReportPeriods()
    {
        var result = _enumService.GetEnumValues<ReportPeriod>();
        return Ok(new ApiResponse<List<EnumGetModel>>(result));
    }

    [HttpGet("payment-operation-types")]
    public async Task<IActionResult> GetPaymentOperationTypes()
    {
        var result = _enumService.GetEnumValues<PaymentOperationType>();
        return Ok(new ApiResponse<List<EnumGetModel>>(result));
    }

    [HttpGet("employee-statuses")]
    public async Task<IActionResult> GetEmployeeStatuses()
    {
        var result = _enumService.GetEnumValues<EmployeeStatus>();
        return Ok(new ApiResponse<List<EnumGetModel>>(result));
    }
}
