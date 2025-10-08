using Marqa.Service.Exceptions;
using Marqa.Service.Services.Employees;
using Marqa.Service.Services.Employees.Models;
using Marqa.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace Marqa.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController(IEmployeeService employeeService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> PostAsync(EmployeeCreateModel model)
    {
        try
        {
            int id = await employeeService.CreateAsync(model);

            return Ok(new Response
            {
                Status = 201,
                Message = $"success employeeId: {id}"
            });
        }
        catch(NotFoundException ex)
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

    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] EmployeeUpdateModel model)
    {
        try
        {
            await employeeService.UpdateAsync(id, model);

            return Ok(new Response
            {
                Status = 200,
                Message = "success"
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
            await employeeService.DeleteAsync(id);

            return Ok(new Response
            {
                Status = 200,
                 Message = "success"
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
            var result = await employeeService.GetAsync(id);

            return Ok(new Response<EmployeeViewModel>
            {
                Status = 200,
                Message = "success",
                Data = result
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

    [HttpGet("companyId{companyId:int}")]
    public async Task<IActionResult> GetAllAsync(int companyId, string search = null)
    {
        try
        {
            var result = await employeeService.GetAllAsync(companyId, search);

            return Ok(new Response<IEnumerable<EmployeeViewModel>>
            {
                Status = 200,   
                Message = "success",
                Data = result
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

    [HttpGet("Teacher/{id:int}")]
    public async Task<IActionResult> GetTeacherAsync(int id)
    {
        try
        {
            var result = await employeeService.GetTeacherAsync(id);

            return Ok(new Response<TeacherViewModel>
            {
                Status = 200,
                Message = "success",
                Data = result
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

    [HttpGet("Teachers/{companyId:int}")]
    public async Task<IActionResult> GetAllTeachersAsync(int companyId, string search=default, int subjectId=default)
    {
        try
        {
            var result = await employeeService.GetAllTeachersAsync(companyId,search,subjectId);

            return Ok(new Response<IEnumerable<TeacherViewModel>>
            {
                Status = 200,
                Message = "success",
                Data = result
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
