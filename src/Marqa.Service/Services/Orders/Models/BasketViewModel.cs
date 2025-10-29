namespace Marqa.Service.Services.Orders.Models;

public class BasketViewModel
{
    public int Id { get; set; }
    public int TotalPrice { get; set; }
    public class ItemIfo
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int InlinePrice { get; set; }
    }
}