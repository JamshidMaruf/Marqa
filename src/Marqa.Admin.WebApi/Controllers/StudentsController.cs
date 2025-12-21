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
    [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
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
    [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
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
    [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
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
    [ProducesResponseType(typeof(Response<StudentViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
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
    [ProducesResponseType(typeof(Response<StudentViewForUpdateModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetForUpdate(int id)
    {
        var student = await studentService.GetForUpdateAsync(id);

        return Ok(new Response<StudentViewForUpdateModel>
        {
            StatusCode = 200,
            Message = "success",
            Data = student
        });
    }

    [HttpPatch("{studentId}/courses/{courseId}/status/{statusId}")]
    [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateStudentCourseStatusAsync(
        int studentId,
        int courseId,
        EnrollmentStatus statusId)
    {
        await studentService.UpdateStudentCourseStatusAsync(studentId, courseId, statusId);
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Student Course status updated successfully",
        });
    }


    [HttpGet("{studentId:int}/courses")]
    //[ProducesResponseType(typeof(Response<IEnumerable<CourseNamesModel>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
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

    [HttpGet("{studentId:int}/active-courses")]
    [ProducesResponseType(typeof(Response<IEnumerable<CourseNamesModel>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetOnlyActiveStudentCoursesAsync(int studentId)
    {
        var students = await courseService.GetActiveStudentCoursesAsync(studentId);
        return Ok(new Response<IEnumerable<CourseNamesModel>>
        {
            StatusCode = 200,
            Message = "success",
            Data = students
        });
    }

    [HttpGet("{studentId:int}/unenrolled-courses")]
    [ProducesResponseType(typeof(Response<List<CourseMinimalListModel>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAvailableCoursesAsync(int companyId, int studentId)
    {
        var result = await courseService.GetUnEnrolledStudentCoursesAsync(companyId, studentId);
        return Ok(new Response<List<CourseMinimalListModel>>
        {
            StatusCode = 200,
            Message = "success",
            Data = result
        });
    }


    [HttpGet]
    [ProducesResponseType(typeof(Response<IEnumerable<StudentListModel>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll(
        [FromQuery] PaginationParams @params,
        [FromQuery] int companyId,
        [FromQuery] string? search = null,
        [FromQuery] int? courseId = null,
        [FromQuery] StudentFilteringStatus? status = null)
    {
        var students = await studentService.GetAllAsync(@params, companyId, search, courseId, status);

        return Ok(new Response<IEnumerable<StudentListModel>>
        {
            StatusCode = 200,
            Message = "success",
            Data = students
        });
    }

    [HttpGet("{companyId}/statistics")]
    public async Task<IActionResult> GetStudentsInfoByCompanyAsync(int companyId)
    {
        var result = await studentService.GetStudentsInfo(companyId);
        return Ok(new Response<StudentsInfo>
        {
            StatusCode = 200,
            Message = "success",
            Data = result
        });
    }
}