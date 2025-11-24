using Marqa.Service.Services.Subjects;
using Marqa.Service.Services.Subjects.Models;
using Marqa.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace Marqa.Teacher.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SubjectsController(ISubjectService subjectService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> PostAsync(SubjectCreateModel model)
    {
        await subjectService.CreateAsync(model);

        return Ok(new Response
        {
            StatusCode = 200,
            Message = "success",
        });
    }

    [HttpPost("attach-teacher")]
    public async Task<IActionResult> PostAsync(int teacherId, int subjectId)
    {
        await subjectService.AttachAsync(teacherId, subjectId);

        return Ok(new Response
        {
            StatusCode = 200,
            Message = "success",
        });
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SubjectUpdateModel model)
    {
        await subjectService.UpdateAsync(id, model);

        return Ok(new Response
        {
            StatusCode = 200,
            Message = "success",
        });
    }

    [HttpDelete("detach-teacher")]
    public async Task<IActionResult> DeleteAsync(int teacherId, int subjectId)
    {
        await subjectService.DetachAsync(teacherId, subjectId);

        return Ok(new Response
        {
            StatusCode = 200,
            Message = "success",
        });
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await subjectService.DeleteAsync(id);

        return Ok(new Response
        {
            StatusCode = 200,
            Message = "success",
        });
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetAsync(int id)
    {
        var subject = await subjectService.GetAsync(id);

        return Ok(new Response<SubjectViewModel>
        {
            StatusCode = 200,
            Message = "success",
            Data = subject
        });
    }

    [HttpGet("{companyId}")]
    public async Task<IActionResult> GetAllAsync(int companyId)
    {
        var subjects = await subjectService.GetAllAsync(companyId);

        return Ok(new Response<IEnumerable<SubjectViewModel>>
        {
            StatusCode = 200,
            Message = "success",
            Data = subjects
        });
    }
}