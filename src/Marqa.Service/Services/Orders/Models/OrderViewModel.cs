using Marqa.Domain.Enums;

namespace Marqa.Service.Services.Orders.Models;

public class OrderViewModel
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public int TotalPrice { get; set; }
    public OrderStatus Status { get; set; }
    public List<OrderItemViewModel> Items { get; set; }
}
