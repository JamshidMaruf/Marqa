namespace Marqa.Domain.Entities;

public class OrderItem : Auditable
{
    public int OrderId {  get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public int InlinePrice {  get; set; }
    public Order Order { get; set; }
    public Product Product { get; set; }
}