namespace Marqa.Service.Services.Products.Models
{
    public class ProductCreateModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int CompanyId { get; set; }
    }
}
