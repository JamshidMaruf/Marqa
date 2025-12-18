using Marqa.Service.Services.Companies.Models;
using Marqa.Shared.Models;

namespace Marqa.Service.Services.Companies;

public interface ICompanyService
{
    Task CreateAsync(CompanyCreateModel model);
    Task UpdateAsync(int id, CompanyUpdateModel model);
    Task DeleteAsync(int id);
    Task<CompanyViewModel> GetAsync(int id);
    Task<List<CompanyViewModel>> GetAllAsync(string? search = null);
    Task<List<CompanyViewModel>> GetAllAsync(PaginationParams @params);
}