using Marqa.Service.Services.Subjects;
using Marqa.Service.Services.Subjects.Models;
using Microsoft.AspNetCore.Mvc;

namespace Marqa.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SubjectsController(ISubjectService subjectService) : ControllerBase
{

    [HttpPost]
    public async Task<IActionResult> PostAsync(SubjectCreateModel model)
    {
        try
        {
            await subjectService.CreateAsync(model);

            return Created();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("TeacherSubject")]
    public async Task<IActionResult> PostAsync(TeacherSubjectCreateModel model)
    {
        try
        {
            await subjectService.CreateAsync(model);

            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SubjectUpdateModel model)
    {
        try
        {
            await subjectService.UpdateAsync(id, model);

            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("TeacherSubject/{id:int}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] int subjectId)
    {
        try
        {
            await subjectService.UpdateAsync(id, subjectId);

            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        try
        {
            await subjectService.DeleteAsync(id);

            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetAsync(int id)
    {
        try
        {
            var subject = await subjectService.GetAsync(id);

            return Ok(subject);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("company/{companyId:int}")]
    public async Task<IActionResult> GetAllAsync(int companyId)
    {
        try
        {
            var subject = await subjectService.GetAllAsync(companyId);

            return Ok(subject);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }




}