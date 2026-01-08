using Marqa.Service.Services.Companies.Models;
using Marqa.Shared.Models;

namespace Marqa.Service.Services.Companies;

public interface ICompanyService : IScopedService
{
    Task CreateAsync(CompanyCreateModel model);
    Task UpdateAsync(int id, CompanyUpdateModel model);
    Task DeleteAsync(int id);
    Task<CompanyViewModel> GetAsync(int id);
    Task<CompanyUpdateFormModel> GetForUpdateAsync(int id);
    Task<List<CompanyViewModel>> GetAllAsync(PaginationParams @params, string search = null);
    Task<int> GetCompaniesCountAsync();
}