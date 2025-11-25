namespace Marqa.Domain.Entities;

public class Product : Auditable
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int Price { get; set; }
    public int CompanyId { get; set; }
    public int AssetId { get; set; }

    public Company Company { get; set; }
    public Asset Asset { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; }
}
