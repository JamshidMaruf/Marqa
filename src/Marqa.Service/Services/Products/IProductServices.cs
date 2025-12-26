using Marqa.Service.Services.Products.Models;
using Marqa.Shared.Models;
using Microsoft.AspNetCore.Http;

namespace Marqa.Service.Services.Products
{
    public interface IProductService : IScopedService
    {
        Task CreateAsync(ProductCreateModel model);
        Task UpdateAsync(int id, ProductUpdateModel model);
        Task DeleteAsync(int id);
        Task<ProductViewModel> GetAsync(int id);
        Task<ProductUpdateFormModel> GetForUpdateAsync(int id);
        Task<List<ProductTableModel>> GetAllAsync(
            int companyId, 
            PaginationParams @params, 
            string search = null);
        Task UploadPictureAsync(int productId, List<IFormFile> files);
        Task RemoveImageAsync(int productId, int imageId);
    }
}
