using Marqa.Service.Services.Employees.Models;

namespace Marqa.Service.Services.Employees;

public interface IEmployeeService
{
    Task<int> CreateAsync(EmployeeCreateModel model);
    Task UpdateAsync(int id, EmployeeUpdateModel model);
    Task DeleteAsync(int id);
    Task<EmployeeViewModel> GetAsync(int id);
    Task<List<EmployeeViewModel>> GetAllAsync(int companyId, string search=default);
    Task<TeacherViewModel> GetTeacherAsync(int id);
    Task<List<TeacherViewModel>> GetAllTeachersAsync(int companyId, string search=null, int subjectId=default);
}
