namespace Marqa.Domain.Entities;

public class BasketItem : Auditable
{
    public int BasketId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public int Price { get; set; }
    public int InlinePrice { get; set; }
    
    public Product Product { get; set; }
    public Basket Basket { get; set; }
}