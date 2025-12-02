using Marqa.Domain.Enums;
using Marqa.Service.Services.Courses;
using Marqa.Service.Services.Courses.Models;
using Marqa.Service.Services.Students;
using Marqa.Service.Services.Students.Models;
using Marqa.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace Marqa.Admin.WebApi.Controllers;
 
public class StudentsController(
    IStudentService studentService, 
    ICourseService courseService) : BaseController
{ 
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] StudentCreateModel model)
    {
        await studentService.CreateAsync(model);

        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Student created successfully",
        });
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] StudentUpdateModel model)
    {
        await studentService.UpdateAsync(id, model);

        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Student updated successfully",
        });
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await studentService.DeleteAsync(id);

        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Student deleted successfully",
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
    public async Task<IActionResult> UpdateStudentCourseStatusAsync(int studentId, int courseId, EnrollmentStatus status)
    {
        await studentService.UpdateStudentCourseStatusAsync(studentId, courseId, status);

        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Student Course status updated successfully",
        });
    }

    [HttpGet("{studentId:int}/courses")]
    public async Task<IActionResult> GetStudentCourses(int studentId)
    {
        var result = await courseService.GetAllStudentCourseNamesAsync(studentId);

        return Ok(new Response<IEnumerable<CourseNamesModel>>
        {
            StatusCode = 200,
            Message = "success",
            Data = result
        });
    }

    [HttpGet("{studentId:int}/unenrolled-courses")]
    public async Task<IActionResult> GetAvailableCoursesAsync(int companyId, int studentId)
    {
        var result = await courseService.GetUnEnrolledStudentCoursesAsync(companyId, studentId);

        return Ok(new Response<List<MinimalCourseDataModel>>
        {
            StatusCode = 200,
            Message = "success",
            Data = result
        });
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] StudentFilterModel filterModel)
    {
        var students = await studentService.GetAllAsync(filterModel);
        return Ok(new Response<IEnumerable<StudentViewModel>>
        {
            StatusCode = 200,
            Message = "success",
            Data = students
        });
    }
}
