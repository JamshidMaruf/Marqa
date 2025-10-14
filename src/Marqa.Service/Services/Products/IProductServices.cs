using Marqa.Service.Services.Product.Models;

namespace Marqa.Service.Services.Products
{
    public interface IProductService
    {
        Task<ProductViewModel> GetAsync(int id);
        Task<List<ProductViewModel>> GetAllAsync(int companyId, string search = null);
        Task CreateAsync(ProductCreateModel model);
        Task UpdateAsync(int id, ProductUpdateModel model);
        Task DeleteAsync(int id);
    }
}
