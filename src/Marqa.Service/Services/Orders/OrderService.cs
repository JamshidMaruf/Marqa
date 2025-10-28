using Marqa.DataAccess.UnitOfWork;
using Marqa.Domain.Entities;
using Marqa.Domain.Enums;
using Marqa.Service.Exceptions;
using Marqa.Service.Services.Orders.Models;
using Microsoft.EntityFrameworkCore;

namespace Marqa.Service.Services.Orders;

public class OrderService(IUnitOfWork unitOfWork) : IOrderService
{
    // TODO: Transaction qo'llash haqida o'ylab ko'rish kerak
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
                Quantity = item.Count,
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

        unitOfWork.Orders.Insert(order);

        await unitOfWork.SaveAsync();

        foreach (var orderItem in orderItems)
        {
            orderItem.OrderId = order.Id;
            unitOfWork.OrderItems.Insert(orderItem);
        }

        await unitOfWork.SaveAsync();

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

        unitOfWork.StudentPointHistories.Insert(pointHistory);

        await unitOfWork.SaveAsync();
    }

    public async Task UpdateStatusAsync(int id, OrderStatus newStatus)
    {
        var existOrder = await unitOfWork.Orders.SelectAsync(o => o.Id == id)
            ?? throw new NotFoundException($"Order not found (ID: {id})");

        existOrder.Status = newStatus;
        unitOfWork.Orders.Update(existOrder);

        await unitOfWork.SaveAsync();
    }

    public async Task DeleteAsync(int id)
    {
        // Order'ni OrderItem'lar bilan birga Include qilish
        var existOrder = await unitOfWork.Orders
            .SelectAllAsQueryable()
            .Include(o => o.OrderItems)
            .FirstOrDefaultAsync(o => o.Id == id)
            ?? throw new NotFoundException($"Order not found (ID: {id})");

        foreach (var item in existOrder.OrderItems)
        {
            unitOfWork.OrderItems.Delete(item);
        }

        unitOfWork.Orders.Delete(existOrder);

        await unitOfWork.SaveAsync();
    }

    public async Task<OrderViewModel> GetAsync(int id)
    {
        // Order'ni OrderItem va Product bilan birga Include qilish
        var order = await unitOfWork.Orders
            .SelectAllAsQueryable()
            .Include(o => o.OrderItems)
                .ThenInclude(i => i.Product)
            .FirstOrDefaultAsync(o => o.Id == id)
            ?? throw new NotFoundException($"Order not found (ID: {id})");

        return new OrderViewModel
        {
            Id = order.Id,
            StudentId = order.StudentId,
            TotalPrice = order.TotalPrice,
            Status = order.Status,
            Items = order.OrderItems.Select(i => new OrderItemViewModel
            {
                ProductId = i.ProductId,
                ProductName = i.Product.Name,
                Count = i.Quantity,
                InlinePrice = i.InlinePrice
            }).ToList()
        };
    }

    public async Task<List<OrderViewModel>> GetAllAsync()
    {
        // Student Include qilmasdan faqat Order ma'lumotlarini olish
        var orders = await unitOfWork.Orders
            .SelectAllAsQueryable()
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