using Marqa.DataAccess.UnitOfWork;
using Marqa.Domain.Entities;
using Marqa.Domain.Enums;
using Marqa.Service.Exceptions;
using Marqa.Service.Services.Orders.Models;
using Microsoft.EntityFrameworkCore;

namespace Marqa.Service.Services.Orders;

public class OrderService(IUnitOfWork unitOfWork) : IOrderService
{
    public async Task CreateAsync(OrderCreateModel model)  
    {
        var student = await unitOfWork.Students.SelectAsync(s => s.Id == model.StudentId)
            ?? throw new NotFoundException($"Student not found (ID: {model.StudentId})");

        var studentPointHistory = await unitOfWork.StudentPointHistories
            .SelectAllAsQueryable()
            .Where(p => p.StudentId == student.Id)
            .OrderByDescending(p => p.CreatedAt)
            .FirstOrDefaultAsync()
            ?? throw new NotFoundException($"Student point history not found (Student ID: {student.Id})");

        var orderItems = new List<OrderItem>();
        int totalPrice = 0;

        foreach (var item in model.Items)
        {
            var product = await unitOfWork.Products.SelectAsync(p => p.Id == item.ProductId)
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

        await unitOfWork.Orders.InsertAsync(order);

        foreach (var orderItem in orderItems)
        {
            orderItem.OrderId = order.Id;
            await unitOfWork.OrderItems.InsertAsync(orderItem);
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

        await unitOfWork.StudentPointHistories.InsertAsync(pointHistory);
    }

    public async Task UpdateStatusAsync(int id, OrderStatus newStatus)
    {
        var existOrder = await unitOfWork.Orders.SelectAsync(o => o.Id == id)
            ?? throw new NotFoundException($"Order not found (ID: {id})");

        existOrder.Status = newStatus;
        await unitOfWork.Orders.UpdateAsync(existOrder);
    }

    public async Task DeleteAsync(int id)
    {
        var existOrder = await unitOfWork.Orders.SelectAsync(o => o.Id == id)
            ?? throw new NotFoundException($"Order not found (ID: {id})");
        
        // orderni ozidan orderitemlarni include qilingani optimalroq bo'ladi
        var orderItems = await unitOfWork.OrderItems
            .SelectAllAsQueryable()
            .Where(i => i.OrderId == id)
            .ToListAsync();

        foreach (var item in orderItems)
        {
            await unitOfWork.OrderItems.DeleteAsync(item);
        }

        await unitOfWork.Orders.DeleteAsync(existOrder);
    }

    public async Task<OrderViewModel> GetAsync(int id)
    {
        // student include qilish keregi yoq studentId alohida property 
        var order = await unitOfWork.Orders.SelectAllAsQueryable()
            .Include(o => o.Student)
            .FirstOrDefaultAsync(o => o.Id == id)
            ?? throw new NotFoundException($"Order not found (ID: {id})");

        // orderItem order bilan birga include qilingan holda ob kelingani optimalroq 
        var items = await unitOfWork.OrderItems.SelectAllAsQueryable()
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
        // student include qilish keremas
        var orders = await unitOfWork.Orders.SelectAllAsQueryable()
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