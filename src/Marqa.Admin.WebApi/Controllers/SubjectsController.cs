using Marqa.Service.Services.Subjects;
using Marqa.Service.Services.Subjects.Models;
using Marqa.Admin.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace Marqa.Admin.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SubjectsController(ISubjectService subjectService) : ControllerBase
{

    [HttpPost("create")]
    public async Task<IActionResult> PostAsync(SubjectCreateModel model)
    {
        await subjectService.CreateAsync(model);

        return Ok(new Response
        {
            Status = 200,
            Message = "success",
        });
    }

    [HttpPost("attach-teacher")]
    public async Task<IActionResult> PostAsync(TeacherSubjectCreateModel model)
    {
        await subjectService.AttachAsync(model);

        return Ok(new Response
        {
            Status = 200,
            Message = "success",
        });
    }

    [HttpPut("update/{id:int}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SubjectUpdateModel model)
    {
        await subjectService.UpdateAsync(id, model);

        return Ok(new Response
        {
            Status = 200,
            Message = "success",
        });
    }

    [HttpDelete("detach-teacher")]
    public async Task<IActionResult> DeleteAsync(int teacherId, int subjectId)
    {
        await subjectService.DetachAsync(teacherId, subjectId);

        return Ok(new Response
        {
            Status = 200,
            Message = "success",
        });
    }

    [HttpDelete("delete/{id:int}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await subjectService.DeleteAsync(id);

        return Ok(new Response
        {
            Status = 200,
            Message = "success",
        });
    }

    [HttpGet("get/{id:int}")]
    public async Task<IActionResult> GetAsync(int id)
    {
        var subject = await subjectService.GetAsync(id);

        return Ok(new Response<SubjectViewModel>
        {
            Status = 200,
            Message = "success",
            Data = subject
        });
    }

    [HttpGet("by{companyId:int}")]
    public async Task<IActionResult> GetAllAsync(int companyId)
    {
        var subjects = await subjectService.GetAllAsync(companyId);

        return Ok(new Response<IEnumerable<SubjectViewModel>>
        {
            Status = 200,
            Message = "success",
            Data = subjects
        });
    }
}