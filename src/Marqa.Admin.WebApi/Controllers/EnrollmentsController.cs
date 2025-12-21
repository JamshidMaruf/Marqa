﻿using Marqa.Service.Services.Courses;
using Marqa.Service.Services.Courses.Models;
using Marqa.Service.Services.Enrollments;
using Marqa.Service.Services.Enrollments.Models;
using Marqa.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace Marqa.Admin.WebApi.Controllers;

public class EnrollmentsController(
    IEnrollmentService enrollmentService,
    ICourseService courseService) : BaseController
{
    [HttpPost("attach")]
    [ProducesResponseType(typeof(Response), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AttachAsync(EnrollmentCreateModel model)
    {
        await enrollmentService.CreateAsync(model);
        return Ok(new Response
        {
            StatusCode = 201,
            Message = "Enrollment has been created successfully"
        });
    }

    [HttpPost("detach")]
    [ProducesResponseType(typeof(Response), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DetachAsync(DetachModel model)
    {
        await enrollmentService.DetachAsync(model);
        return Ok(new Response
        {
            StatusCode = 201,
            Message = "Enrollment has been deleted successfully"
        });
    }

    [HttpPost("freeze")]
    [ProducesResponseType(typeof(Response), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> FreezeAsync(FreezeModel model)
    {
        await enrollmentService.FreezeStudentAsync(model);
        return Ok(new Response
        {
            StatusCode = 201,
            Message = "Enrollment has been frozen successfully"
        });
    }

    [HttpGet("frozen-courses")]
    [ProducesResponseType(typeof(Response<List<FrozenEnrollmentModel>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetFrozenCoursesAsync(int studentId)
    {
        var result = await courseService.GetFrozenCoursesAsync(studentId);

        return Ok(new Response<List<FrozenEnrollmentModel>>
        {
            StatusCode = 200,
            Message = "success",
            Data = result
        });
    }

    [HttpPost("unfreeze")]
    [ProducesResponseType(typeof(Response), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UnfreezeAsync(UnFreezeModel model)
    {
        await enrollmentService.UnFreezeStudentAsync(model);
        return Ok(new Response
        {
            StatusCode = 201,
            Message = "Enrollment has been unfrozen successfully"
        });
    }

    [HttpPost("transfer")]
    [ProducesResponseType(typeof(Response), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> TransferAsync(StudentTransferModel model)
    {
        await enrollmentService.MoveStudentCourseAsync(model);
        return Ok(new Response
        {
            StatusCode = 201,
            Message = "Student has been transferred successfully"
        });
    }

    [HttpGet("{studentId:int}/active-courses")]
    [ProducesResponseType(typeof(Response<IEnumerable<NonFrozenEnrollmentModel>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetOnlyActiveStudentCoursesAsync(int studentId)
    {
        var students = await courseService.GetActiveStudentCoursesAsync(studentId);
        return Ok(new Response<IEnumerable<NonFrozenEnrollmentModel>>
        {
            StatusCode = 200,
            Message = "success",
            Data = students
        });
    }

    [HttpGet("specific-enrollment-statuses")]
    [ProducesResponseType(typeof(Response<EnrollmentStatusViewModel>), StatusCodes.Status200OK)]
    public IActionResult GetSpecificEnrollmentStatuses()
    {
        var result = enrollmentService.GetSpecificEnrollmentStatuses();
        return Ok(new Response<EnrollmentStatusViewModel>
        {
            StatusCode = 200,
            Message = "success",
            Data = result
        });
    }
}