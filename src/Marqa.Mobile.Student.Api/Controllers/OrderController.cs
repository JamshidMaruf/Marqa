using Marqa.Domain.Enums;
using Marqa.Mobile.Student.Api.Models;
using Marqa.Service.Services.Orders;
using Marqa.Service.Services.Orders.Models;
using Microsoft.AspNetCore.Mvc;

namespace Marqa.Mobile.Student.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController(IOrderService orderService) : ControllerBase
{
    [HttpPost("basket")]
    public async Task<IActionResult> PostBasketAsync(int studentId)
    {
        await orderService.CreateBasketAsync(studentId);

        return Ok(new Response
        {
            StatusCode = 201,
            Message = "success",
        });
    }

    [HttpPost("basket-item")]
    public async Task<IActionResult> PostBasketItemAsync(BasketItemCreateModel model)
    {
        await orderService.CreateBasketItemAsync(model);

        return Ok(new Response
        {
            StatusCode = 200,
            Message = "success"
        });
    }

    [HttpPost("checkout")]
    public async Task<IActionResult> CheckOutAsync(int basketId)
    {
        await orderService.CheckoutAsync(basketId);

        return Ok(new Response
        {
            StatusCode = 200,
            Message = "success"
        });
    }

    [HttpPut("{orderId:int}")]
    public async Task<IActionResult> UpdateStatusAsync(int orderId, OrderStatus newStatus)
    {
        await orderService.UpdateStatusAsync(orderId, newStatus);

        return Ok(new Response
        {
            StatusCode = 200,
            Message = "success"
        });
    }

    [HttpDelete("basket-item")]
    public async Task<IActionResult> DeleteBasketItemAsync(BasketItemDeleteModel model)
    {
        await orderService.DeleteBasketItemAsync(model);

        return Ok(new Response
        {
            StatusCode = 200,
            Message = "success"
        });
    }

    [HttpGet("basket{stduentId:int}")]
    public async Task<IActionResult> GetAsync(int studentId)
    {
        var basket = await orderService.GetBasketByStudentIdAsync(studentId);

        return Ok(new Response<BasketViewModel>
        {
            StatusCode = 200,
            Message = "success",
            Data = basket
        });
    }

    [HttpGet("orders/{id:int}")]
    public async Task<IActionResult> GetOrderAsync(int id)
    {
        var order = await orderService.GetOrderByIdAsync(id);

        return Ok(new Response<OrderViewModel>
        {
            StatusCode = 200,
            Message = "success",
            Data = order
        });
    }

    [HttpGet("orders{studentId:int}")]
    public async Task<IActionResult> GetStudentOrdersAsync(int studentId)
    {
        var order = await orderService.GetOrdersByStudentIdAsync(studentId);

        return Ok(new Response<List<OrderViewModel>>
        {
            StatusCode = 200,
            Message = "success",
            Data = order
        });
    }
}
