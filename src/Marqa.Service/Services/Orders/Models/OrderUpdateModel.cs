using Marqa.Domain.Enums;

namespace Marqa.Service.Services.Orders.Models;
public class OrderUpdateModel
{
    public int OrderId { get; set; }
    public OrderStatus Status { get; set; }
}
