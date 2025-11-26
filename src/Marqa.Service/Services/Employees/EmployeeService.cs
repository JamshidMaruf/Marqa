using FluentValidation;
using Marqa.DataAccess.UnitOfWork;
using Marqa.Domain.Entities;
using Marqa.Domain.Enums;
using Marqa.Service.Exceptions;
using Marqa.Service.Extensions;
using Marqa.Service.Helpers;
using Marqa.Service.Services.Employees.Models;
using Microsoft.EntityFrameworkCore;

namespace Marqa.Service.Services.Employees;

public class EmployeeService(IUnitOfWork unitOfWork,
    IValidator<EmployeeCreateModel> validatorEmployeeCreate,
    IValidator<EmployeeUpdateModel> validatorEmployeeUpdate) : IEmployeeService
{
    public async Task<Employee> CreateAsync(EmployeeCreateModel model)
    {
        await validatorEmployeeCreate.EnsureValidatedAsync(model);

        _ = await unitOfWork.Companies.SelectAsync(c => c.Id == model.CompanyId)
           ?? throw new NotFoundException("Company was not found");

        _ = await unitOfWork.EmployeeRoles.SelectAsync(e => e.Id == model.RoleId && e.CompanyId == model.CompanyId)
            ?? throw new NotFoundException($"No employee role was found with ID = {model.RoleId}");

        var alreadyExistEmployee = await unitOfWork.Employees.SelectAsync(e => e.User.Phone == model.Phone && e.CompanyId == model.CompanyId);

        if (alreadyExistEmployee != null)
            throw new AlreadyExistException($"Employee with this phone {model.Phone} already exists");

        var employeePhone = model.Phone.TrimPhoneNumber();
        if (!employeePhone.IsSuccessful)
            throw new ArgumentIsNotValidException("Invalid phone number!");

        var userEntity = unitOfWork.Users.Insert(new User()
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            Phone = employeePhone.Phone,
            Email = model.Email,
            PasswordHash = PasswordHelper.Hash(model.Password),
            Role = UserRole.Employee
        });
        await unitOfWork.SaveAsync();
        
        var createdEmployee = unitOfWork.Employees.Insert(new Employee
        {
            UserId = userEntity.Id,
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

        return createdEmployee;
    }

    public async Task UpdateAsync(int id, EmployeeUpdateModel model)
    {
        await validatorEmployeeUpdate.EnsureValidatedAsync(model);

        var existEmployee = await unitOfWork.Employees.SelectAsync(e => e.Id == id, includes: "User")
            ?? throw new NotFoundException($"Employee was not found");

        _ = await unitOfWork.EmployeeRoles.SelectAsync(e => e.Id == model.RoleId && e.CompanyId == existEmployee.CompanyId)
            ?? throw new NotFoundException($"No employee role was found with ID = {model.RoleId}");

        var existPhone = await unitOfWork.Employees.SelectAsync(e => 
                            e.User.Phone == model.Phone && 
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
            .SelectAllAsQueryable(e => !e.IsDeleted && !e.Role.CanTeach)
            .FirstOrDefaultAsync()
            ?? throw new NotFoundException($"Employee was not found with ID = {id}");

        unitOfWork.Employees.MarkAsDeleted(employeeForDeletion);

        await unitOfWork.SaveAsync();
    }

    public async Task<EmployeeViewModel> GetAsync(int id)
    {
        return await unitOfWork.Employees
            .SelectAllAsQueryable(e => !e.IsDeleted && !e.Role.CanTeach,
            includes: [ "Role", "User" ])
            .Select(e => new EmployeeViewModel
            {
                Id = e.Id,
                FirstName = e.User.Phone,
                LastName = e.User.Phone,
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
            .SelectAllAsQueryable(e => !e.IsDeleted && !e.Role.CanTeach,
            includes: ["Role", "User"])
            .Select(e => new EmployeeViewModel
            {
                Id = e.Id,
                FirstName = e.User.Phone,
                LastName = e.User.Phone,
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

    public async Task<List<EmployeeViewModel>> GetAllAsync(int companyId, string search)
    {
        var employees = unitOfWork.Employees
            .SelectAllAsQueryable(e => e.CompanyId == companyId && !e.IsDeleted && !e.Role.CanTeach);

        if (!string.IsNullOrWhiteSpace(search))
            employees = employees.Where(e =>
                e.User.FirstName.Contains(search) ||
                e.User.LastName.Contains(search) ||
                e.User.Phone.Contains(search) ||
                e.User.Email.Contains(search) ||
                e.Specialization.Contains(search));

        return await employees.Select(e => new EmployeeViewModel
        {
            Id = e.Id,
            FirstName = e.User.FirstName,
            LastName = e.User.Phone,
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
        }).ToListAsync();
    }

    public async Task<EmployeeLoginViewModel> LoginAsync(EmployeeLoginModel model)
    {
        var employee = await unitOfWork.Employees
            .SelectAsync(predicate: e => e.User.Phone == model.Phone, includes: ["Role", "User"])
            ?? throw new ArgumentIsNotValidException("Phone or Password is invalid.");

        if (!PasswordHelper.Verify(model.Password, employee.User.PasswordHash))
            throw new ArgumentIsNotValidException("Phone or Password is invalid.");

        //var token = await authService.GenerateEmployeeTokenAsync(employee.Id, employee.Role.Name);

        var employeePermissions = await unitOfWork.RolePermissions
            .SelectAllAsQueryable(predicate: r => r.RoleId == employee.RoleId, includes: "Permission")
            .Select(r => new EmployeeLoginViewModel.PermissionInfo { Name = r.Permission.Name })
            .ToListAsync();

        return new EmployeeLoginViewModel
        {
            Id = employee.Id,
            FirstName = employee.User.Phone,
            LastName = employee.User.LastName,
            Phone = employee.User.Phone,
            CompanyId = employee.CompanyId,
            Role = employee.Role.Name,
            //Token = token,
            Permissions = employeePermissions
        };
    }
}
