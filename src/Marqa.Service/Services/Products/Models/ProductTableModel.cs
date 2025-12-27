namespace Marqa.Service.Services.Products.Models
{
    public class ProductTableModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int CompanyId { get; set; }
        public bool IsDisplayed { get; set; }
    }
}
