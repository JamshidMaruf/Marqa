using Marqa.Domain.Enums;

namespace Marqa.Domain.Entities;

public class Order:Auditable
{
    public int StudentId { get; set; }
    public int TotalPrice { get; set; }
    public OrderStatus Status { get; set; }

    public Student Student { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; }
}
