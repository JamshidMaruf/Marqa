﻿using Marqa.Domain.Entities;
using Marqa.Service.Services.Employees.Models;
using Marqa.Service.Services.Teachers.Models;
using Marqa.Shared.Models;

namespace Marqa.Service.Services.Employees;

public interface IEmployeeService : IScopedService
{
    Task<Employee> CreateAsync(EmployeeCreateModel model);
    Task UpdateAsync(int id, EmployeeUpdateModel model);
    Task DeleteAsync(int id);
    Task<EmployeeViewModel> GetAsync(int id);
    Task<EmployeeUpdateViewModel> GetForUpdateAsync(int id);
    Task<int> GetByPhoneAsync(string phone);
    Task<EmployeesStatistic> GetStatisticsAsync(int companyId);
    Task<List<EmployeeViewModel>> GetAllAsync(PaginationParams @params, int companyId, string search = null);
}
