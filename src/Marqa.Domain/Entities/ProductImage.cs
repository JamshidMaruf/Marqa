namespace Marqa.Domain.Entities;

public class ProductImage : Auditable
{
    public int ProductId { get; set; }
    public int ImageId { get; set; }

    public Product Product { get; set; }
    public Asset Image { get; set; }
}
