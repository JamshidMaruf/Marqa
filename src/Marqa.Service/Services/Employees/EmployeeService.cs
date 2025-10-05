using Marqa.DataAccess.Repositories;
using Marqa.Domain.Entities;
using Marqa.Service.Exceptions;
using Marqa.Service.Extensions;
using Marqa.Service.Services.Employees.Models;
using Microsoft.EntityFrameworkCore;

namespace Marqa.Service.Services.Employees;

public class EmployeeService(
    IRepository<Company> companyRepository,
    IRepository<Employee> employeeRepository,
    IRepository<TeacherSubject> teacherSubjectRepository,
    IRepository<Course> courseRepository)
    : IEmployeeService
{
    public async Task<int> CreateAsync(EmployeeCreateModel model)
    {
        _ = await companyRepository.SelectAsync(model.CompanyId)
           ?? throw new NotFoundException("Company was not found");

        var createdEmp = await employeeRepository.InsertAsync(new Employee
        {
            CompanyId = model.CompanyId,
            FirstName = model.FirstName,
            LastName = model.LastName,
            DateOfBirth = model.DateOfBirth,
            Gender = model.Gender,
            Phone = model.Phone,
            Email = model.Email,
            Status = model.Status,
            PasswordHash = model.Password.Hash(),
            JoiningDate = model.JoiningDate,
            Specialization = model.Specialization,
            Info = model.Info
        });

        return createdEmp.Id;
    }

    public async Task UpdateAsync(int id, EmployeeUpdateModel model)
    {
        var existTeacher = await employeeRepository.SelectAsync(id)
            ?? throw new NotFoundException($"Employee was not found");

        existTeacher.FirstName = model.FirstName;
        existTeacher.LastName = model.LastName;
        existTeacher.DateOfBirth = model.DateOfBirth;
        existTeacher.Gender = model.Gender;
        existTeacher.Phone = model.Phone;
        existTeacher.Email = model.Email;
        existTeacher.Status = model.Status;
        existTeacher.JoiningDate = model.JoiningDate;
        existTeacher.Specialization = model.Specialization;

        await employeeRepository.UpdateAsync(existTeacher);
    }

    public async Task DeleteAsync(int id)
    {
        var existEmployee = await employeeRepository.SelectAsync(id)
            ?? throw new NotFoundException($"Employee was not found");

        await employeeRepository.DeleteAsync(existEmployee);
    }

    public async Task<EmployeeViewModel> GetAsync(int id)
    {
        return await employeeRepository
            .SelectAllAsQueryable()
            .Include(e => e.Role)
            .Select(e => new EmployeeViewModel
            {
                Id = e.Id,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Phone = e.Phone,
                Email = e.Email,
                DateOfBirth = e.DateOfBirth,
                Gender = e.Gender,
                Status = e.Status,
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

    public async Task<List<EmployeeViewModel>> GetAllAsync(int companyId, string search)
    {
        var employees = employeeRepository
            .SelectAllAsQueryable()
            .Where(e => e.CompanyId == companyId && !e.IsDeleted);

        if (string.IsNullOrWhiteSpace(search))
            employees = employees.Where(e =>
                e.FirstName.Contains(search) ||
                e.LastName.Contains(search) ||
                e.Phone.Contains(search) ||
                e.Email.Contains(search) ||
                e.Specialization.Contains(search));

        return await employees.Select(e => new EmployeeViewModel
        {
            Id = e.Id,
            FirstName = e.FirstName,
            LastName = e.LastName,
            Phone = e.Phone,
            Email = e.Email,
            DateOfBirth = e.DateOfBirth,
            Gender = e.Gender,
            Status = e.Status,
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

    public async Task<TeacherViewModel> GetTeacherAsync(int id)
    {
        var teacher = await teacherSubjectRepository
           .SelectAllAsQueryable()
           .Where(ts => ts.TeacherId == id)
           .Include(ts => ts.Teacher)
           .Include(ts => ts.Subject)
           .Select(ts => new TeacherViewModel
           {
               Id = ts.Teacher.Id,
               DateOfBirth = ts.Teacher.DateOfBirth,
               Gender = ts.Teacher.Gender,
               FirstName = ts.Teacher.FirstName,
               LastName = ts.Teacher.LastName,
               Email = ts.Teacher.Email,
               Phone = ts.Teacher.Phone,
               Status = ts.Teacher.Status,
               JoiningDate = ts.Teacher.JoiningDate,
               Specialization = ts.Teacher.Specialization,
               Subject = new TeacherViewModel.SubjectInfo
               {
                   Id = ts.Id,
                   Name = ts.Subject.Name
               }
           })
           .FirstOrDefaultAsync()
            ?? throw new NotFoundException($"No teacher was found with ID = {id}.");

        var courses = await courseRepository.SelectAllAsQueryable()
            .Include(c => c.Subject)
            .Where(c => c.TeacherId == id)
            .Select(c => new TeacherViewModel.CourseInfo
            {
                Id = c.Id,
                Name = c.Name,
                SubjectId = c.Subject.Id,
                SubjectName = c.Subject.Name
            })
            .ToListAsync();

        teacher.Courses = courses;

        return teacher;
    }

    public Task<List<TeacherViewModel>> GetAllTeachersAsync(int companyId, string search = null, int? subjectId = null)
    {
        throw new NotImplementedException();
    }


    public async Task<List<TeacherViewModel>> GetAllTeachersAsync(int companyId, string search=default, int subjectId=default)
    {
        var teacherQuery = teacherSubjectRepository
            .SelectAllAsQueryable()
            .Include(ts => ts.Teacher)
            .Include(ts => ts.Subject)
            .Where(ts => ts.Teacher.CompanyId == companyId)
            .Select(t => new TeacherViewModel
            {
                Id = t.TeacherId,
                DateOfBirth = t.Teacher.DateOfBirth,
                Gender = t.Teacher.Gender,
                FirstName = t.Teacher.FirstName,
                LastName = t.Teacher.LastName,
                Email = t.Teacher.Email,
                Phone = t.Teacher.Phone,
                Status = t.Teacher.Status,
                JoiningDate = t.Teacher.JoiningDate,
                Specialization = t.Teacher.Specialization,
                Subject = new TeacherViewModel.SubjectInfo
                {
                    Id = t.SubjectId,
                    Name = t.Subject.Name
                }
            });

        if (!string.IsNullOrWhiteSpace(search))
        {
            search = search.ToLower();
            teacherQuery = teacherQuery.Where(t =>
                t.FirstName.ToLower().Contains(search) ||
                t.LastName.ToLower().Contains(search) ||
                t.Specialization.ToLower().Contains(search) ||
                t.Phone.Contains(search) ||
                t.Email.Contains(search));
        }

        if (subjectId != null)
            teacherQuery = teacherQuery.Where(t => t.Subject.Id == subjectId);

        var teachers = await teacherQuery.ToListAsync();

        var teacherCourses = teachers
            .GroupJoin(
                courseRepository.SelectAllAsQueryable()
                .Include(c => c.Subject),
                t => t.Id,
                c => c.TeacherId,
                (t, courses) => new TeacherViewModel // shu joyini optimizatsiya qilish kerak
                {
                    Id = t.Id,
                    DateOfBirth = t.DateOfBirth,
                    Gender = t.Gender,
                    FirstName = t.FirstName,
                    LastName = t.LastName,
                    Email = t.Email,
                    Phone = t.Phone,
                    Status = t.Status,
                    JoiningDate = t.JoiningDate,
                    Specialization = t.Specialization,
                    Subject = t.Subject,
                    Courses = courses.Select(c => new TeacherViewModel.CourseInfo
                    {
                        Id = c.Id,
                        Name = c.Name,
                        SubjectId = c.Subject.Id,
                        SubjectName = c.Subject.Name
                    }).ToList()
                }
            ).ToList();

        return teacherCourses;
    }
}
