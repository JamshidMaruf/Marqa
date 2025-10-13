using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Marqa.DataAccess.Repositories;
using Marqa.Domain.Entities;
using Marqa.Service.Exceptions;
using Marqa.Service.Services.PointSettings.Models;
using Marqa.Service.Services.Product.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;


namespace Marqa.Service.Services.Product
{
    public class ProductService : IProductService
    {

        private readonly IProductRepository _productRepository;
        private readonly ICompanyRepository _companyRepository;

        public ProductService(
            IProductRepository productRepository,
            ICompanyRepository companyRepository)
        {
            _productRepository = productRepository;
            _companyRepository = companyRepository;
        }
        public async Task<ProductViewModel> CreateAsync(CreateProduct dto)
        {
            var companyExists = await _companyRepository.ExistsAsync(dto.CompanyId);

            if (!companyExists)
                throw new InvalidOperationException($"Company with ID {dto.CompanyId} does not exist");

            var product = new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                CompanyId = dto.CompanyId
            };

            var createdProduct = await _productRepository.AddAsync(product);
            return MapToViewModel(createdProduct);
        }
        public async Task<ProductViewModel> UpdateAsync(int id, UpdateProduct dto)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product == null)
                return null;

            product.Name = dto.Name;
            product.Description = dto.Description;
            product.Price = dto.Price;

            var updatedProduct = await _productRepository.UpdateAsync(product);
            return MapToViewModel(updatedProduct);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _productRepository.DeleteAsync(id);
        }

        public async Task<ProductViewModel> GetAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product == null)
                return null;

            return MapToViewModel(product);
        }

        public async Task<IEnumerable<ProductViewModel>> GetAllAsync(string search = null)
        {
            var products = await _productRepository.GetAllAsync(search);
            return products.Select(MapToViewModel);
        }

    }

}

   