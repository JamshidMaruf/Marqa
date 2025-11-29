using Marqa.Service.Services.EmployeePayments;
using Marqa.Service.Services.EmployeePayments.Models;
using Microsoft.AspNetCore.Mvc;

namespace Marqa.Admin.WebApi.Controllers;

[ApiController]
[Route("api/employee-payments")]
public class EmployeePaymentsController(IEmployeePaymentService employeePaymentService) : ControllerBase
{
    /// <summary>
    /// Create employee payment
    /// POST: api/employee-payments
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] EmployeePaymentCreateModel model)
    {
        await employeePaymentService.CreateAsync(model);

        return Ok(new
        {
            status = 200,
            message = "success"
        });
    }

    /// <summary>
    /// Get employee payment for update
    /// GET: api/employee-payments/{id}/update
    /// </summary>
    [HttpGet("{id}/update")]
    public async Task<IActionResult> GetForUpdateAsync([FromRoute] int id)
    {
        var payment = await employeePaymentService.GetByPaymentIdAsync(id);

        return Ok(new
        {
            status = 200,
            message = "success",
            data = new
            {
                employee = new
                {
                    id = payment.EmployeeId,
                },
                paymentMethod = new
                {
                    id = (int)payment.PaymentMethod,
                    name = payment.PaymentMethod.ToString()
                },
                amount = payment.Amount.ToString("0"),
                dateTime = payment.DateTime.ToString("yyyy-MM-dd"),
                description = payment.Description,
                paymentOperationType = new
                {
                    id = (int)payment.EmployeePaymentOperationType,
                    name = payment.EmployeePaymentOperationType.ToString()
                }
            }
        });
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] EmployeePaymentUpdateModel model)
    {
        model.Id = id;
        await employeePaymentService.UpdateAsync(model);

        return Ok(new { status = 200, message = "success" });
    }

    /// <summary>
    /// Get employee payment by ID
    /// GET: api/employee-payments/{id}
    /// </summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
    {
        var payment = await employeePaymentService.GetByPaymentIdAsync(id);

        return Ok(new
        {
            status = 200,
            message = "success",
            data = new
            {
                employee = new
                {
                    id = payment.EmployeeId,
                },
                paymentMethod = new
                {
                    id = (int)payment.PaymentMethod,
                    name = payment.PaymentMethod.ToString()
                },
                amount = payment.Amount.ToString("0"),
                dateTime = payment.DateTime.ToString("yyyy-MM-dd"),
                description = payment.Description,
                paymentOperationType = new
                {
                    id = (int)payment.EmployeePaymentOperationType,
                    name = payment.EmployeePaymentOperationType.ToString()
                }
            }
        });
    }

    /// <summary>
    /// Get all employee payments with filters
    /// GET: api/employees-payments?search=test&employeeId=1&pageIndex=1&pageSize=10
    /// </summary>
    [HttpGet]
    [Route("/api/employees-payments")]
    public async Task<IActionResult> GetAllAsync(
        [FromQuery] string? search = null,
        [FromQuery] int? employeeId = null,
        [FromQuery] int pageIndex = 1,
        [FromQuery] int pageSize = 10)
    {
        var payments = await employeePaymentService.GetAllAsync(search, employeeId, pageIndex, pageSize);

        var result = payments.Select(p => new
        {
            id = p.Id,
            paymentMethod = p.PaymentMethod,
            amount = p.Amount.ToString("0"),
            dateTime = p.DateTime.ToString("yyyy-MM-dd"),
            paymentOperationType = p.EmployeePaymentOperationType,
            employee = new
            {
                id = p.EmployeeId,
            }
        }).ToList();

        return Ok(new
        {
            status = 200,
            message = "success",
            data = result
        });
    }
}
