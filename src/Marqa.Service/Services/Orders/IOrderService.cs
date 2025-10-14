using Marqa.Domain.Enums;
using Marqa.Service.Services.Orders.Models;

namespace Marqa.Service.Services.Orders;

public interface IOrderService
{
    Task CreateAsync(OrderCreateModel model);
    Task UpdateStatusAsync(int id, OrderStatus newStatus);
    Task DeleteAsync(int id);
    Task<OrderViewModel> GetAsync(int id);
    Task<List<OrderViewModel>> GetAllAsync();
}
