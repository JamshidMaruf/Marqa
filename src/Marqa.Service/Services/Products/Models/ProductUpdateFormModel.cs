namespace Marqa.Service.Services.Products.Models
{
    public class ProductUpdateFormModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public CompanyInfo Company { get; set; }
        public bool IsDisplayed { get; set; }

        public class CompanyInfo
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}
