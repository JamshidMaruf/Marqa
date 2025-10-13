using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Marqa.Service.Services.Product.Models;

namespace Marqa.Service.Services.Product
{
    public interface IProductService
    {
        Task<ProductViewModel> GetAsync(int id);
        Task<IEnumerable<ProductViewModel>> GetAllAsync(string search = null);
        Task<ProductViewModel> CreateAsync(CreateProduct dto );
        Task<ProductViewModel> UpdateAsync(int id, UpdateProduct dto );
        Task<bool> DeleteAsync(int id);
    }
}
