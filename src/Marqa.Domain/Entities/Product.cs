using Marqa.Domain.Enums;
namespace Marqa.Domain.Entities
{
    public class Product : Auditable
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int CompanyId { get; set; }
    }
}
