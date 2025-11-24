using Marqa.Service.Services.Students;
using Marqa.Service.Services.Students.Models;
using Marqa.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace Marqa.Admin.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController(IStudentService studentService) : ControllerBase
{
    [HttpPost()]
    public async Task<IActionResult> PostAsync([FromBody] StudentCreateModel model)
    {
        await studentService.CreateAsync(model);

        return Ok(new Response
        {
            StatusCode = 200,
            Message = "success",
        });
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] StudentUpdateModel model)
    {
        await studentService.UpdateAsync(id, model);

        return Ok(new Response
        {
            StatusCode = 200,
            Message = "success",
        });
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await studentService.DeleteAsync(id);

        return Ok(new Response
        {
            StatusCode = 200,
            Message = "success",
        });
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetAsync(int id)
    {
        var student = await studentService.GetAsync(id);

        return Ok(new Response<StudentViewModel>
        {
            StatusCode = 200,
            Message = "success",
            Data = student
        });
    }

    [HttpGet("{id}/update")]
    public async Task<IActionResult> GetForUpdate(int id)
    {
        var student = await studentService.GetAsync(id);

        return Ok(new Response<StudentViewModel>
        {
            StatusCode = 200,
            Message = "success",
            Data = student
        });
    }

    [HttpPut("{studentId}/courses/{courseId}/status{statusId}")]
    public async Task<IActionResult> UpdateStudentCourseStatusAsync(int studentId, int courseId, int statusId)
    {
        await studentService.UpdateStudentCourseStatusAsync(studentId, courseId, statusId);
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "success",
        });
    }

    [HttpGet()]
    public async Task<IActionResult> GetAll([FromQuery] StudentFilterModel filterModel)
    {
        var students = await studentService.GetAll(filterModel);
        return Ok(new Response<List<StudentViewModel>>
        {
            StatusCode = 200,
            Message = "success",
            Data = students
        });
    }
}
