using System.ComponentModel;
using Marqa.Service.Services.Employees.EmployeeServices.Models;

namespace Marqa.Service.Services.Employees.EmployeeServices;

public interface IEmployeeService
{
    Task CreateAsync(EmployeeCreateModel model);
    Task UpdateAsync(int id, EmployeeUpdateModel model);
    Task DeleteAsync(int id);
    Task<EmployeeViewModel> GetAsync(int id);
    Task<List<EmployeeViewModel>> GetAllAsync(int companyId, string search);
    Task<TeacherViewModel> GetTeacherAsync(int id);
    Task<List<TeacherViewModel>> GetAllTeachersAsync(int companyId, string search, int? subjectId);
}
