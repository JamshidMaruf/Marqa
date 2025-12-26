using FluentValidation;
using Marqa.Domain.Entities;
using Marqa.Domain.Enums;
using Marqa.Service.Exceptions;
using Marqa.Service.Extensions;
using Marqa.Service.Helpers;
using Marqa.Service.Services.Employees.Models;
using Marqa.Shared.Helpers;
using Marqa.Shared.Models;
using Marqa.Shared.Services;
using Microsoft.EntityFrameworkCore;

namespace Marqa.Service.Services.Employees;

public class EmployeeService(IUnitOfWork unitOfWork,
    IValidator<EmployeeCreateModel> validatorEmployeeCreate,
    IValidator<EmployeeUpdateModel> validatorEmployeeUpdate,
    IPaginationService paginationService) : IEmployeeService
{
    public async Task<Employee> CreateAsync(EmployeeCreateModel model)
    {
        await validatorEmployeeCreate.EnsureValidatedAsync(model);
        
        var employeePhone = model.Phone.TrimPhoneNumber();
        if (!employeePhone.IsSuccessful)
            throw new ArgumentIsNotValidException("Invalid phone number!");
        
        var existEmployeePhone = await unitOfWork.Employees.SelectAsync(e => e.User.Phone == employeePhone.Phone && e.CompanyId == model.CompanyId);
        if (existEmployeePhone != null)
            throw new AlreadyExistException($"Employee with this phone {model.Phone} already exists");
        
        var transaction = await unitOfWork.BeginTransactionAsync();
        try
        {
            var user = unitOfWork.Users.Insert(new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Phone = employeePhone.Phone,
                Email = model.Email,
                PasswordHash = PasswordHelper.Hash(model.Password),
                Role = UserRole.Employee,
                IsActive = true,
                IsUseSystem = model.IsUseSystem
            });
            await unitOfWork.SaveAsync();

            var createdEmployee = unitOfWork.Employees.Insert(new Employee
            {
                User = user,
                UserId = user.Id,
                CompanyId = model.CompanyId,
                DateOfBirth = model.DateOfBirth,
                Gender = model.Gender,
                Status = model.Status,
                Salary = model.Salary,
                JoiningDate = model.JoiningDate,
                Specialization = model.Specialization,
                Info = model.Info,
                RoleId = model.RoleId
            });

            await unitOfWork.SaveAsync();
            await transaction.CommitAsync();
            return createdEmployee;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task UpdateAsync(int id, EmployeeUpdateModel model)
    {
        await validatorEmployeeUpdate.EnsureValidatedAsync(model);

        var existEmployee = await unitOfWork.Employees.SelectAsync(e => e.Id == id, includes: "User")
            ?? throw new NotFoundException($"Employee was not found");

        var existEmployeeRole = await unitOfWork.EmployeeRoles.CheckExistAsync(e => e.Id == model.RoleId && e.CompanyId == existEmployee.CompanyId);

        if (!existEmployeeRole)
            throw new NotFoundException($"No employee role was found with ID = {model.RoleId}");
        var trimmedPhone = model.Phone.TrimPhoneNumber();
        var existPhone = await unitOfWork.Employees.SelectAsync(e =>
                            e.User.Phone == trimmedPhone.Phone &&
                            e.CompanyId == existEmployee.CompanyId &&
                            e.Id != id, includes: "User");

        if (existPhone != null)
            throw new AlreadyExistException($"Employee with this phone {model.Phone} already exists");

        var employeePhone = model.Phone.TrimPhoneNumber();
        if (!employeePhone.IsSuccessful)
            throw new ArgumentIsNotValidException("Invalid phone number!");

        existEmployee.User.FirstName = model.FirstName;
        existEmployee.User.LastName = model.LastName;
        existEmployee.DateOfBirth = model.DateOfBirth;
        existEmployee.Gender = model.Gender;
        existEmployee.User.Phone = employeePhone.Phone;
        existEmployee.User.Email = model.Email;
        existEmployee.Status = model.Status;
        existEmployee.Salary = model.Salary;
        existEmployee.JoiningDate = model.JoiningDate;
        existEmployee.Specialization = model.Specialization;
        existEmployee.RoleId = model.RoleId;

        await unitOfWork.SaveAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var employeeForDeletion = await unitOfWork.Employees
            .SelectAllAsQueryable(e => !e.IsDeleted)
            .FirstOrDefaultAsync()
            ?? throw new NotFoundException($"Employee was not found with ID = {id}");

        unitOfWork.Employees.MarkAsDeleted(employeeForDeletion);

        await unitOfWork.SaveAsync();
    }

    public async Task<EmployeeViewModel> GetAsync(int id)
    {
        return await unitOfWork.Employees
            .SelectAllAsQueryable()
            .Select(e => new EmployeeViewModel
            {
                Id = e.Id,
                CompanyId = e.CompanyId,
                FirstName = e.User.FirstName,
                LastName = e.User.LastName,
                Phone = e.User.Phone,
                Email = e.User.Email,
                DateOfBirth = e.DateOfBirth,
                Gender = e.Gender,
                Status = e.Status,
                Salary = e.Salary,
                JoiningDate = e.JoiningDate,
                Specialization = e.Specialization,
                Info = e.Info,
                RoleId = e.RoleId,
                Role = new EmployeeViewModel.EmployeeRoleInfo
                {
                    Id = e.RoleId,
                    Name = e.Role.Name
                }
            })
            .FirstOrDefaultAsync(e => e.Id == id)
            ?? throw new NotFoundException($"No employee was found with ID = {id}");
    }

    public async Task<EmployeeViewModel> GetForUpdateAsync(int id)
    {
        return await unitOfWork.Employees
            .SelectAllAsQueryable()
            .Select(e => new EmployeeViewModel
            {
                Id = e.Id,
                CompanyId = e.CompanyId,
                FirstName = e.User.FirstName,
                LastName = e.User.LastName,
                Phone = e.User.Phone,
                Email = e.User.Email,
                DateOfBirth = e.DateOfBirth,
                Gender = e.Gender,
                Status = e.Status,
                Salary = e.Salary,
                JoiningDate = e.JoiningDate,
                Specialization = e.Specialization,
                Info = e.Info,
                RoleId = e.RoleId,
                Role = new EmployeeViewModel.EmployeeRoleInfo
                {
                    Id = e.RoleId,
                    Name = e.Role.Name
                }
            })
            .FirstOrDefaultAsync(e => e.Id == id)
            ?? throw new NotFoundException($"No employee was found with ID = {id}");
    }

    public async Task<int> GetByPhoneAsync(string phone)
    {
        var employee = await unitOfWork.Employees.SelectAsync(emp => emp.User.Phone == phone);

        return employee.Id;
    }

    public async Task<List<EmployeeViewModel>> GetAllAsync(PaginationParams @params, int companyId, string search)
    {
        var employees = unitOfWork.Employees
            .SelectAllAsQueryable(e => e.CompanyId == companyId);

        if (!string.IsNullOrWhiteSpace(search))
            employees = employees.Where(e =>
                e.User.FirstName.Contains(search) ||
                e.User.LastName.Contains(search) ||
                e.User.Phone.Contains(search) ||
                e.User.Email.Contains(search) ||
                e.Specialization.Contains(search));
        var pagedEmployees = await paginationService.Paginate(employees, @params).ToListAsync();
        
        return pagedEmployees.Select(e => new EmployeeViewModel
        {
            Id = e.Id,
            CompanyId = e.CompanyId,
            FirstName = e.User.FirstName,
            LastName = e.User.LastName,
            Phone = e.User.Phone,
            Email = e.User.Email,
            DateOfBirth = e.DateOfBirth,
            Gender = e.Gender,
            Status = e.Status,
            Salary = e.Salary,
            JoiningDate = e.JoiningDate,
            Specialization = e.Specialization,
            Info = e.Info,
            RoleId = e.RoleId,
            Role = new EmployeeViewModel.EmployeeRoleInfo
            {
                Id = e.RoleId,
                Name = e.Role.Name
            }
        }).ToList();
    }
}
