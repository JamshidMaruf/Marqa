using Marqa.Service.Services.Students;
using Marqa.Service.Services.Students.Models;
using Marqa.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace Marqa.Teacher.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController(IStudentService studentService) : ControllerBase
{
    [HttpPost("create")]
    public async Task<IActionResult> PostAsync(StudentCreateModel model)
    {
        await studentService.CreateAsync(model);

        return Ok(new Response
        {
            StatusCode = 200,
            Message = "success",
        });
    }

    [HttpPut("update/{id:int}")]
    public async Task<IActionResult> PutAsync(int id, int companyId, [FromBody] StudentUpdateModel model)
    {
      //  await studentService.UpdateAsync(id, companyId, model);

        return Ok(new Response
        {
            StatusCode = 200,
            Message = "success",
        });
    }

    [HttpDelete("delete/{id:int}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await studentService.DeleteAsync(id);

        return Ok(new Response
        {
            StatusCode = 200,
            Message = "success",
        });
    }

    [HttpGet("get/{id:int}")]
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

    [HttpGet("courseStudents/{courseId:int}")]
    public async Task<IActionResult> GetAllAsync(int courseId)
    {
        var students = await studentService.GetAllByCourseIdAsync(courseId);

        return Ok(new Response<List<StudentViewModel>>
        {
            StatusCode = 200,
            Message = "success",
            Data = students,
        });
    }
}
