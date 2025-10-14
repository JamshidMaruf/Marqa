namespace Marqa.Service.Services.Orders.Models;

public class OrderItemViewModel
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public int Count { get; set; }
    public int InlinePrice { get; set; }
}
