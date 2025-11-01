﻿using Marqa.DataAccess.UnitOfWork;
using Marqa.Domain.Entities;
using Marqa.Domain.Enums;
using Marqa.Service.Exceptions;
using Marqa.Service.Services.Orders.Models;
using Microsoft.EntityFrameworkCore;

namespace Marqa.Service.Services.Orders;

public class OrderService(IUnitOfWork unitOfWork) : IOrderService
{
    public async Task CreateBasketAsync(int studentId)
    {
        _ = await unitOfWork.Students.SelectAsync(s => s.Id == studentId)
            ?? throw new NotFoundException("Student not found");
        
        unitOfWork.Baskets.Insert(new Basket
        {
            StudentId = studentId   
        });

        await unitOfWork.SaveAsync();
    }

    public async Task CreateBasketItemAsync(BasketItemCreateModel model)
    {
        var basket = await unitOfWork.Baskets.SelectAsync(b => b.Id == model.BasketId)
            ?? throw new NotFoundException("Basket not found");

        var existBasketItem = await unitOfWork.BasketItems.SelectAsync(b =>
            b.BasketId == model.BasketId &&
            b.ProductId == model.ProductId);

        if (existBasketItem != null)
        {
            existBasketItem.Quantity += model.Quantity;
            unitOfWork.BasketItems.Update(existBasketItem);
            
            basket.TotalPrice += model.InlinePrice;
            unitOfWork.Baskets.Update(basket);
        }
        else
        {
            unitOfWork.BasketItems.Insert(new BasketItem()
            {
                BasketId = model.BasketId,
                ProductId = model.ProductId,
                Quantity = model.Quantity,
                InlinePrice = model.InlinePrice
            });

            basket.TotalPrice += model.InlinePrice;
            unitOfWork.Baskets.Update(basket);
        }
        
        await unitOfWork.SaveAsync();
    }

    public async Task DeleteBasketItemAsync(BasketItemDeleteModel model)
    {
        var basket = await unitOfWork.Baskets.SelectAsync(
                predicate: b => b.Id == model.BasketId, 
                includes: new []{ "BasketItems" })
            ?? throw new NotFoundException("Basket not found");

        var basketItem = basket.BasketItems.FirstOrDefault(b => b.ProductId == model.ProductId);
        
        if (basketItem != null)
        {
            if (basketItem.Quantity == model.Quantity)
            {
                unitOfWork.BasketItems.Delete(basketItem);

                basket.TotalPrice -= model.InlinePrice;
                unitOfWork.Baskets.Update(basket);
            }
            else if(basketItem.Quantity > model.Quantity)
            {
                basketItem.Quantity -= model.Quantity;
                unitOfWork.BasketItems.Update(basketItem);
                
                basket.TotalPrice -= model.InlinePrice;
                unitOfWork.Baskets.Update(basket);
            }
            
            await unitOfWork.SaveAsync();
        }
    }

    public async Task<BasketViewModel> GetBasketByStudentIdAsync(int studentId)
    {
        var basket = await unitOfWork.Baskets.SelectAsync(
            predicate: b => b.StudentId == studentId,
            includes: new[] { "BasketItems" })
            ?? throw new NotFoundException("Basket not found");

        return new BasketViewModel
        {
            Id = basket.Id,
            TotalPrice = basket.TotalPrice,
            Items = basket.BasketItems.Select(bi => new BasketViewModel.ItemIfo
            {
                ProductId = bi.ProductId,
                Quantity = bi.Quantity,
                InlinePrice = bi.InlinePrice
            }).ToList()
        };
    }

    public async Task CheckoutAsync(int basketId)
    {
        var basket = await unitOfWork.Baskets.SelectAsync(
                         predicate: b => b.Id == basketId, 
                         includes: new []{ "BasketItems" })
                     ?? throw new NotFoundException("Basket not found");
        
        
        var createdOrder = unitOfWork.Orders.Insert(new Order
        {
            StudentId = basket.StudentId,
            TotalPrice = basket.TotalPrice,
            Status = OrderStatus.InProcess,
            Number = GenerateOrderNumber().Result,
        });
        
        await unitOfWork.SaveAsync();

        var orderItems = basket.BasketItems.Select(bi => new OrderItem
        {
            ProductId = bi.ProductId, 
            Quantity = bi.Quantity, 
            InlinePrice = bi.InlinePrice,
            OrderId = createdOrder.Id
        });

        await unitOfWork.OrderItems.InsertRangeAsync(orderItems);

        await unitOfWork.SaveAsync();
    }

    public async Task<OrderViewModel> GetOrderByIdAsync(int id)
    {
        var order = await unitOfWork.Orders.SelectAllAsQueryable()
            .Include(o => o.OrderItems)
            .ThenInclude(ot => ot.Product)
            .FirstOrDefaultAsync(o => o.Id == id)
            ?? throw new NotFoundException($"No order was found with ID = {id}");

        return new OrderViewModel
        {
            Id = order.Id,
            Number = order.Number,
            TotalPrice = order.TotalPrice,
            Status = order.Status,
            OrderItems = order.OrderItems.Select(ot => new OrderViewModel.ItemInfo
            {
                ProductId = ot.Product.Id,
                ProductName = ot.Product.Name,
                ProductImageName = ot.Product.ImageName,
                ProductImagePage = ot.Product.ImagePage,
                ProductImageExtension = ot.Product.ImageExtension,
                ProductDescription = ot.Product.Description,
                Quantity = ot.Quantity,
                Price = ot.Price,
                InlinePrice = ot.InlinePrice
            }).ToList()
        };
    }

    public async Task<List<OrderViewModel>> GetOrdersByStudentIdAsync(int studentId)
    {
        var orders = await unitOfWork.Orders.SelectAllAsQueryable()
            .Include(o => o.OrderItems)
            .ThenInclude(ot => ot.Product)
            .ToListAsync();

        return orders.Select(order => new OrderViewModel
        {
            Id = order.Id,
            Number = order.Number,
            TotalPrice = order.TotalPrice,
            Status = order.Status,
            OrderItems = order.OrderItems.Select(ot => new OrderViewModel.ItemInfo
            {
                ProductId = ot.Product.Id,
                ProductName = ot.Product.Name,
                ProductImageName = ot.Product.ImageName,
                ProductImagePage = ot.Product.ImagePage,
                ProductImageExtension = ot.Product.ImageExtension,
                ProductDescription = ot.Product.Description,
                Quantity = ot.Quantity,
                Price = ot.Price,
                InlinePrice = ot.InlinePrice
            }).ToList()
        }).ToList();
    }

    public async Task UpdateStatusAsync(int orderId, OrderStatus newStatus)
    {
        var order = await unitOfWork.Orders.SelectAsync(t => t.Id == orderId)
            ?? throw new NotFoundException("Order not found");
        
        order.Status = newStatus;
        unitOfWork.Orders.Update(order);
        await unitOfWork.SaveAsync();
    }

    private async Task<long> GenerateOrderNumber()
    {
        var orders = await unitOfWork.Orders
            .SelectAllAsQueryable()
            .OrderByDescending(o => o.Number)
            .Select(o => new
            {
                Number = o.Number
            })
            .ToListAsync();

        if (orders.Count == 0)
            return 00000001;
        else
            return orders[0].Number + 1;
    }
}