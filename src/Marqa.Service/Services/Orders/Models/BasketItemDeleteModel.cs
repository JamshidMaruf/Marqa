namespace Marqa.Service.Services.Orders.Models;

public class BasketItemDeleteModel
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public int InlinePrice { get; set; }
    public int BasketId { get; set; }
}