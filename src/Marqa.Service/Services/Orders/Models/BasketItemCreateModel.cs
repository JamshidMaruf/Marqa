namespace Marqa.Service.Services.Orders.Models;

public class BasketItemCreateModel
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public int BasketId { get; set; }
}