﻿using Marqa.Domain.Enums;
using Marqa.Service.Services.Orders.Models;

namespace Marqa.Service.Services.Orders;

public interface IOrderService : IScopedService
{
    Task CreateBasketAsync(int studentId);
    Task CreateBasketItemAsync(BasketItemCreateModel model);
    Task DeleteBasketItemAsync(BasketItemDeleteModel model);
    Task<BasketViewModel> GetBasketByStudentIdAsync(int studentId);
    Task CheckoutAsync(int basketId);
    Task<OrderViewModel> GetOrderByIdAsync(int id);
    Task<List<OrderViewModel>> GetOrdersByStudentIdAsync(int studentId);
    Task UpdateStatusAsync(int orderId, OrderStatus newStatus);
}