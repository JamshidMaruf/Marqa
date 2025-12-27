using Marqa.Domain.Enums;
using Marqa.Service.Exceptions;
using Marqa.Service.Services.Teachers;
using Marqa.Service.Services.Teachers.Models;
using Marqa.Shared.Models;
using Microsoft.AspNetCore.Mvc; 

namespace Marqa.Admin.WebApi.Controllers;

public class TeachersController(ITeacherService teacherService) : BaseController 
{
    [HttpPost]
    public async Task<IActionResult> PostAsync(TeacherCreateModel model)
    {
        await teacherService.CreateAsync(model);

        return Ok(new Response
        {
            StatusCode = 201,
            Message = "Teacher created successfully"
        });
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] TeacherUpdateModel model)
    {
        await teacherService.UpdateAsync(id, model);

        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Teacher updated successfully"
        });
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await teacherService.DeleteAsync(id);

        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Teacher deleted successfully"
        });
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(Response<TeacherViewModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAsync(int id)
    {
        var result = await teacherService.GetAsync(id);

        return Ok(new Response<TeacherViewModel>
        {
            StatusCode = 200,
            Message = "success",
            Data = result
        });
    }

    [HttpGet("{id:int}/update")]
    [ProducesResponseType(typeof(Response<TeacherUpdateViewModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetForUpdateAsync(int id)
    {
        var result = await teacherService.GetForUpdateAsync(id);

        return Ok(new Response<TeacherUpdateViewModel>
        {
            StatusCode = 200,
            Message = "success",
            Data = result
        });
    }

    [HttpGet("company/{companyId:int}")]
    [ProducesResponseType(typeof(Response<List<TeacherTableViewModel>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllTeachersAsync(
        int companyId, 
        [FromQuery] PaginationParams @params, 
        [FromQuery] string? search, 
        [FromQuery] TeacherStatus? status)
    {
        var result = await teacherService.GetAllAsync(companyId, @params, search, status);

        return Ok(new Response<List<TeacherTableViewModel>>
        {
            StatusCode = 200,
            Message = "success",
            Data = result
        });
    }
    
    [HttpGet("minimal-list/company/{companyId:int}")]
    [ProducesResponseType(typeof(Response<List<TeacherMinimalListModel>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTeacherListAsync(int companyId)
    {
        var result = await teacherService.GetMinimalListAsync(companyId);

        return Ok(new Response<List<TeacherMinimalListModel>>
        {
            StatusCode = 200,
            Message = "success",
            Data = result
        });
    }
    
    [HttpGet("teachers/payment-types")]
    [ProducesResponseType(typeof(Response<List<TeacherPaymentGetModel>>), StatusCodes.Status200OK)]
    public IActionResult GetTeacherPaymentTypes()
    {
        var result = teacherService.GetTeacherPaymentTypes();
        return Ok(new Response<List<TeacherPaymentGetModel>>
        {
            StatusCode = 200,
            Message = "success",
            Data = result
        });
    }
    [HttpGet("company/{companyId:int}/statistics")]
    [ProducesResponseType(typeof(Response<TeachersStatistics>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetStatistics(int companyId)
    {
        var statistics = await teacherService.GetStatisticsAsync(companyId);

        return Ok(new Response<TeachersStatistics>
        {
            StatusCode = 200,
            Message = "success",
            Data = statistics
        });
    }
}

