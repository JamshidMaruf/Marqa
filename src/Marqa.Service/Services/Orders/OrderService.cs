using Marqa.DataAccess.UnitOfWork;
using Marqa.Domain.Entities;
using Marqa.Domain.Enums;
using Marqa.Service.Exceptions;
using Marqa.Service.Services.Orders.Models;
using Microsoft.EntityFrameworkCore;

namespace Marqa.Service.Services.Orders;

public class OrderService(IUnitOfWork unitOfWork) : IOrderService
{
    public Task CreateBasketAsync(int studentId)
    {
        throw new NotImplementedException();
    }

    public Task CreateBasketItemAsync(BasketItemCreateModel model)
    {
        throw new NotImplementedException();
    }

    public Task DeleteBasketItemAsync(BasketItemDeleteModel model)
    {
        throw new NotImplementedException();
    }

    public Task<BasketViewModel> GetBasketByStudentIdAsync(int studentId)
    {
        throw new NotImplementedException();
    }

    public Task CheckoutAsync(int basketId)
    {
        throw new NotImplementedException();
    }

    public Task<OrderViewModel> GetOrderByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<OrderViewModel>> GetOrdersByStudentIdAsync(int studentId)
    {
        throw new NotImplementedException();
    }

    public Task UpdateStatusAsync(int orderId, OrderStatus newStatus)
    {
        throw new NotImplementedException();
    }
}