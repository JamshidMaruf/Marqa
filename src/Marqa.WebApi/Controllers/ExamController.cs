using Marqa.Service.Services.Exams;
using Marqa.Service.Services.Exams.Models;
using Marqa.Service.Services.Students;
using Marqa.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace Marqa.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExamController(IExamService examService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] ExamCreateModel model)
    {
        await examService.CreateExamAsync(model);
        return Ok(new Response
        {
            Status = 200,
            Message = "success",
        });
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] ExamUpdateModel model)
    {
        await examService.UpdateExamAsync(id, model);
        return Ok(new Response
        {
            Status = 200,
            Message = "success",
        });
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await examService.DeleteExamAsync(id);
        return Ok(new Response
        {
            Status = 200,
            Message = "success",
        });
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] string? search, [FromQuery] int? courseid)
    {
        var exams = await examService.GetAllExamsAsync(search, courseid);
        return Ok(new Response<IEnumerable<ExamViewModel>>
        {
            Status = 200,
            Message = "success",
            Data = exams
        });
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetAsync(int id)
    {
        var exam = await examService.GetExamByIdAsync(id);
        return Ok(new Response<ExamViewModel>
        {
            Status = 200,
            Message = "success",
            Data = exam
        });
    }

    [HttpPost("score")]
    public async Task<IActionResult> ScoreExamAsync([FromBody] StudentExamResultCreate model)
    {
        await examService.ScoreExam(model);
        return Ok(new Response
        {
            Status = 200,
            Message = "success",
        });
    }

    [HttpPut("score/{id:int}")]
    public async Task<IActionResult> UpdateScoreExamAsync(int id, [FromBody] StudentExamResultUpdate model)
    {
        await examService.UpdateExamScore(id, model);
        return Ok(new Response
        {
            Status = 200,
            Message = "success",
        });
    }

    [HttpGet("examResults/{studentId:int}")]
    public async Task<IActionResult> GetExamResultsAsync(int studentId)
    {
        var examResults = await examService.GetExamResultsByStudentIdAsync(studentId);
        return Ok(new Response<List<StudentExamResultView>>
        {
            Status = 200,
            Message = "success",
            Data = examResults,
        });
    }
}
