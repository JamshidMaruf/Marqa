using Marqa.Domain.Enums;

namespace Marqa.Service.Services.Orders;

public class OrderViewModel
{
    public int Id { get; set; }
    public string Number { get; set; }
    public int TotalPrice { get; set; }
    public OrderStatus Status { get; set; }
    public List<ItemInfo> OrderItems { get; set; }
    
    public class ItemInfo
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductImageName { get; set; }
        public string ProductImagePath { get; set; }
        public string ProductImageExtension { get; set; }
        public string ProductDescription { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public int InlinePrice {  get; set; }
    }
}