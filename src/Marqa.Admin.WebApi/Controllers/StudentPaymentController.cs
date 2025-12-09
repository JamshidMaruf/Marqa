using System.Net;
using Marqa.Service.DTOs.StudentPaymentOperations;
using Marqa.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Marqa.Admin.WebApi.Controllers;

[ApiController]
[Route("api/student-payments")]
public class StudentPaymentController : ControllerBase
{
    private readonly IStudentPaymentService _studentPaymentService;

    public StudentPaymentController(IStudentPaymentService studentPaymentService)
    {
        _studentPaymentService = studentPaymentService;
    }

    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> Create(CreatePaymentModel model)
    {
        await _studentPaymentService.CreateAsync(model);
        return Ok(new { status = 200, message = "success" });
    }

    [HttpPut("{id}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdatePaymentModel model)
    {
        model.PaymentId = id;
        await _studentPaymentService.UpdateAsync(model);
        return Ok(new { status = 200, message = "success" });
    }

    [HttpPatch("{id}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> Cancel([FromBody] CancelPaymentModel model)
    {
        await _studentPaymentService.CancelAsync(model);
        return Ok(new { status = 200, message = "success" });
    }

    [HttpPut("{id}/refund")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> Refund([FromBody] CancelPaymentModel model)
    {
        await _studentPaymentService.RefundAsync(model);
        return Ok(new { status = 200, message = "success" });
    }

    [HttpPut("{id}/transfer")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> Transfer([FromBody] TransferPaymentModel model)
    {
        await _studentPaymentService.TransferAsync(model);
        return Ok(new { status = 200, message = "success" });
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(StudentPaymentViewModel), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _studentPaymentService.GetByIdAsync(id);
        return Ok(new { status = 200, message = "success", data = result });
    }

    [HttpGet("{id}/update")]
    [ProducesResponseType(typeof(StudentPaymentViewModel), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetForUpdate(int id)
    {
        var result = await _studentPaymentService.GetByIdAsync(id);
        return Ok(new { status = 200, message = "success", data = result });
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<StudentPaymentListViewModel>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetAll(
        [FromQuery] string search,
        [FromQuery] int pageIndex = 1,
        [FromQuery] int pageSize = 10)
    {
        var results = await _studentPaymentService.GetAllAsync(search, pageIndex, pageSize);

        return Ok(new { status = 200, message = "success", data = results });
    }
}