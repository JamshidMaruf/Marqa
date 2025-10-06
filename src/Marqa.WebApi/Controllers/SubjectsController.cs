using Marqa.Service.Exceptions;
using Marqa.Service.Services.Subjects;
using Marqa.Service.Services.Subjects.Models;
using Marqa.WebApi.Models;
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

            return Ok(new Response
            {
                Status = 200,
                Message = "success",
            });
        }
        catch (AlreadyExistException ex)
        {
            return BadRequest(new Response
            {
                Status = ex.StatusCode,
                Message = ex.Message,
            });
        }
        catch (NotFoundException ex)
        {
            return BadRequest(new Response
            {
                Status = ex.StatusCode,
                Message = ex.Message
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new Response
            {
                Status = 400,
                Message = ex.Message
            });
        }
    }

    [HttpPost("TeacherSubject")]
    public async Task<IActionResult> PostAsync(TeacherSubjectCreateModel model)
    {
        try
        {
            await subjectService.AttachAsync(model);

            return Ok(new Response
            {
                Status = 200,
                Message = "success",
            });
        }
        catch (NotFoundException ex)
        {
            return BadRequest(new Response
            {
                Status = ex.StatusCode,
                Message = ex.Message
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new Response
            {
                Status = 500,
                Message = ex.Message
            });
        }
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SubjectUpdateModel model)
    {
        try
        {
            await subjectService.UpdateAsync(id, model);

            return Ok(new Response
            {
                Status = 200,
                Message = "success",
            });
        }
        catch (NotFoundException ex)
        {
            return BadRequest(new Response
            {
                Status = ex.StatusCode,
                Message = ex.Message
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new Response
            {
                Status = 500,
                Message = ex.Message
            });
        }

    }

    [HttpPut("TeacherSubject/{id:int}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] int subjectId)
    {
        try
        {
            await subjectService.EditAttachedSubjectAsync(id, subjectId);

            return Ok(new Response
            {
                Status = 200,
                Message = "success",
            });
        }
        catch (NotFoundException ex)
        {
            return BadRequest(new Response
            {
                Status = ex.StatusCode,
                Message = ex.Message
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new Response
            {
                Status = 500,
                Message = ex.Message
            });
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        try
        {
            await subjectService.DeleteAsync(id);

            return Ok(new Response
            {
                Status = 200,
                Message = "success",
            });
        }
        catch (NotFoundException ex)
        {
            return BadRequest(new Response
            {
                Status = ex.StatusCode,
                Message = ex.Message
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new Response
            {
                Status = 500,
                Message = ex.Message
            });
        }
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetAsync(int id)
    {
        try
        {
            var subject = await subjectService.GetAsync(id);

            return Ok(new Response<SubjectViewModel>
            {
                Status = 200,
                Message = "success",
                Data = subject
            });
        }
        catch (NotFoundException ex)
        {
            return BadRequest(new Response
            {
                Status = ex.StatusCode,
                Message = ex.Message
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new Response
            {
                Status = 500,
                Message = ex.Message
            });
        }
    }

    [HttpGet("company/{companyId:int}")]
    public async Task<IActionResult> GetAllAsync(int companyId)
    {
        try
        {
            var subject = await subjectService.GetAllAsync(companyId);

            return Ok(new Response<IEnumerable<SubjectViewModel>>
            {
                Status = 200,
                Message = "success",
                Data = subject
            });
        }
        catch (NotFoundException ex)
        {
            return BadRequest(new Response
            {
                Status = ex.StatusCode,
                Message = ex.Message
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new Response
            {
                Status = 500,
                Message = ex.Message
            });
        }
    }
}