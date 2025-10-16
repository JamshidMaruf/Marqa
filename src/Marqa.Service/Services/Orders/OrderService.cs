using Marqa.DataAccess.Repositories;
using Marqa.Domain.Entities;
using Marqa.Domain.Enums;
using Marqa.Service.Exceptions;
using Marqa.Service.Services.Orders.Models;
using Microsoft.EntityFrameworkCore;

namespace Marqa.Service.Services.Orders;

public class OrderService(
    IRepository<Order> orderRepository,
    IRepository<OrderItem> orderItemRepository,
    IRepository<Product> productRepository,
    IRepository<Student> studentRepository,
    IRepository<StudentPointHistory> pointHistoryRepository
) : IOrderService
{
    public async Task CreateAsync(OrderCreateModel model)  
    {
        var student = await studentRepository.SelectAsync(model.StudentId)
            ?? throw new NotFoundException($"Student not found (ID: {model.StudentId})");

        var studentPointHistory = await pointHistoryRepository
            .SelectAllAsQueryable()
            .Where(p => p.StudentId == student.Id)
            .OrderByDescending(p => p.CreatedAt)
            .FirstOrDefaultAsync()
            ?? throw new NotFoundException($"Student point history not found (Student ID: {student.Id})");

        var orderItems = new List<OrderItem>();
        int totalPrice = 0;

        foreach (var item in model.Items)
        {
            var product = await productRepository.SelectAsync(item.ProductId)
                ?? throw new NotFoundException($"Product not found (ID: {item.ProductId})");

            if (item.Count <= 0)
                throw new ArgumentIsNotValidException($"Invalid product count (Product ID: {item.ProductId})");

            int inlinePrice = product.Price * item.Count;
            totalPrice += inlinePrice;

            orderItems.Add(new OrderItem
            {
                ProductId = item.ProductId,
                Count = item.Count,
                InlinePrice = inlinePrice
            });
        }

        if (studentPointHistory.CurrentPoint < totalPrice)
            throw new ArgumentIsNotValidException(
                $"Insufficient points! Required: {totalPrice}, Available: {studentPointHistory.CurrentPoint}");

        var order = new Order
        {
            StudentId = model.StudentId,
            TotalPrice = totalPrice,
        };

        await orderRepository.InsertAsync(order);

        foreach (var orderItem in orderItems)
        {
            orderItem.OrderId = order.Id;
            await orderItemRepository.InsertAsync(orderItem);
        }

        var newCurrentPoint = studentPointHistory.CurrentPoint - totalPrice;

        var pointHistory = new StudentPointHistory
        {
            StudentId = student.Id,
            PreviousPoint = studentPointHistory.CurrentPoint,
            GivenPoint = totalPrice,
            CurrentPoint = newCurrentPoint,
            Note = $"Spent {totalPrice} points for order #{order.Id}",
            Operation = PointHistoryOperation.Minus
        };

        await pointHistoryRepository.InsertAsync(pointHistory);
    }

    public async Task UpdateStatusAsync(int id, OrderStatus newStatus)
    {
        var existOrder = await orderRepository.SelectAsync(id)
            ?? throw new NotFoundException($"Order not found (ID: {id})");

        existOrder.Status = newStatus;
        await orderRepository.UpdateAsync(existOrder);
    }

    public async Task DeleteAsync(int id)
    {
        var existOrder = await orderRepository.SelectAsync(id)
            ?? throw new NotFoundException($"Order not found (ID: {id})");

        var orderItems = await orderItemRepository
            .SelectAllAsQueryable()
            .Where(i => i.OrderId == id)
            .ToListAsync();

        foreach (var item in orderItems)
        {
            await orderItemRepository.DeleteAsync(item);
        }

        await orderRepository.DeleteAsync(existOrder);
    }

    public async Task<OrderViewModel> GetAsync(int id)
    {
        var order = await orderRepository.SelectAllAsQueryable()
            .Include(o => o.Student)
            .FirstOrDefaultAsync(o => o.Id == id)
            ?? throw new NotFoundException($"Order not found (ID: {id})");

        var items = await orderItemRepository.SelectAllAsQueryable()
            .Include(i => i.Product) 
            .Where(i => i.OrderId == order.Id)
            .ToListAsync();

        return new OrderViewModel
        {
            Id = order.Id,
            StudentId = order.StudentId,
            TotalPrice = order.TotalPrice,
            Status = order.Status,
            Items = items.Select(i => new OrderItemViewModel
            {
                ProductId = i.ProductId,
                ProductName = i.Product.Name, 
                Count=i.Count,
                InlinePrice = i.InlinePrice
            }).ToList()
        };
    }

    public async Task<List<OrderViewModel>> GetAllAsync()
    {
        var orders = await orderRepository.SelectAllAsQueryable()
            .Include(o => o.Student)
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync();

        return orders.Select(o => new OrderViewModel
        {
            Id = o.Id,
            StudentId = o.StudentId,
            TotalPrice = o.TotalPrice,
            Status = o.Status,
        }).ToList();
    }
}