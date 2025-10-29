namespace Marqa.Domain.Entities;

public class Product : Auditable
{
    public int CompanyId { get; set; }
    public string Name { get; set; }
    public string ImageName { get; set; }
    public string ImagePage { get; set; }
    public string ImageExtension { get; set; }
    public string Description { get; set; }
    public int Price { get; set; }
    public Company Company { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; }
}
