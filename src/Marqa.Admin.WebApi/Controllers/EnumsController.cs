﻿using Marqa.Domain.Enums;
using Marqa.Service.Services.Enums;
using Marqa.Service.Services.Enums.Models;
using Marqa.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace Marqa.Admin.WebApi.Controllers;

public class EnumsController : BaseController
{
    private readonly IEnumService _enumService;

    public EnumsController(IEnumService enumService)
    {
        _enumService = enumService;
    }

    [HttpGet("genders")]
    public IActionResult GetGenders()
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
    public IActionResult GetReportPeriods()
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
    public IActionResult GetPaymentOperationTypes()
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
    public IActionResult GetEmployeeStatuses()
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
    public IActionResult GetEnrollmentStatus()
    {
        var result = _enumService.GetEnumValues<EnrollmentStatus>();
        return Ok(new Response<List<EnumGetModel>>
        {
            StatusCode = 200,
            Message = "success",
            Data = result
        });
    }

    [HttpGet("course-payment-type")]
    public IActionResult GetCoursePaymentType()
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
    public IActionResult GetEmployeePaymentOperationType()
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
    public IActionResult GetPaymentMethod()
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
    public IActionResult GetStudentStatus()
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
    public IActionResult GetYearlyMonths()
    {
        var result = _enumService.GetCurrentYearlyMonths();

        return Ok(new Response<YearlyMonths>
        {
            StatusCode = 200,
            Message = "success",
            Data = result
        });
    }
    
    [HttpGet("student-filtering-status")]
    public IActionResult GetStudentFilteringStatus()
    {
        var result = _enumService.GetEnumValues<StudentFilteringStatus>();

        return Ok(new Response<List<EnumGetModel>>
        {
            StatusCode = 200,
            Message = "success",
            Data = result
        });
    }
    
    [HttpGet("teacher-types")]
    public IActionResult GetTeacherTypes()
    {
        var result = _enumService.GetEnumValues<TeacherType>();

        return Ok(new Response<List<EnumGetModel>>
        {
            StatusCode = 200,
            Message = "success",
            Data = result
        });
    }
    
    [HttpGet("teacher-salary-types")]
    public IActionResult GetTeacherSalaryTypes()
    {
        var result = _enumService.GetEnumValues<TeacherSalaryType>();

        return Ok(new Response<List<EnumGetModel>>
        {
            StatusCode = 200,
            Message = "success",
            Data = result
        });
    }
    
    [HttpGet("teacher-statuses")]
    public IActionResult GetTeacherStatuses()
    {
        var result = _enumService.GetEnumValues<TeacherStatus>();

        return Ok(new Response<List<EnumGetModel>>
        {
            StatusCode = 200,
            Message = "success",
            Data = result
        });
    }
}
