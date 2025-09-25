using Marqa.Service.Services.EmployeeRoles.Models;

namespace Marqa.Service.Services.EmployeeRoles;
public interface IEmployeeRoleService
{
    Task CreateAsync(EmployeeRoleCreateModel model);
    Task UpdateAsync(int id, EmployeeRoleUpdateModel model);
    Task DeleteAsync(int id);

    Task<EmployeeRoleViewModel> GetAsync(int id);
    Task<List<EmployeeRoleViewModel>> GetAllAsync(int? companyid);
}
