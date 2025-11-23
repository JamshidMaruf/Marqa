using Marqa.Domain.Entities;
using Marqa.Service.Services.Employees.Models;
using Marqa.Service.Services.Teachers.Models;

namespace Marqa.Service.Services.Employees;

public interface IEmployeeService
{
    Task<Employee> CreateAsync(EmployeeCreateModel model);
    Task UpdateAsync(int id, EmployeeUpdateModel model);
    Task DeleteAsync(int id);
    Task<EmployeeViewModel> GetAsync(int id);
    Task<int> GetByPhoneAsync(string phone);
    Task<List<EmployeeViewModel>> GetAllAsync(int companyId, string search = null);
    Task<EmployeeLoginViewModel> LoginAsync(EmployeeLoginModel model);
}
