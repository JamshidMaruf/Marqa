using Marqa.DataAccess.Repositories;
using Marqa.Domain.Entities;
using Marqa.Service.Exceptions;
using Marqa.Service.Extensions;
using Marqa.Service.Services.Employees.EmployeeServices.Models;
using Microsoft.EntityFrameworkCore;

namespace Marqa.Service.Services.Employees.EmployeeServices;

public class EmployeeService : IEmployeeService
{
    private readonly IRepository<Company> companyRepository;
    private readonly IRepository<Employee> employeeRepository;
    private readonly IRepository<TeacherSubject> teacherSubjectRepository;
    private readonly IRepository<Course> courseRepository;

    public EmployeeService()
    {
        companyRepository = new Repository<Company>();
        employeeRepository = new Repository<Employee>();
        teacherSubjectRepository = new Repository<TeacherSubject>();
        courseRepository = new Repository<Course>();
    }

    public async Task CreateAsync(EmployeeCreateModel model)
    {
        _ = await companyRepository.SelectAsync(model.CompanyId)
           ?? throw new NotFoundException("Company was not found");


        await employeeRepository.InsertAsync(new Employee
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
        {
            employees = employees.Where(e =>
                e.FirstName.Contains(search) ||
                e.LastName.Contains(search) ||
                e.Phone.Contains(search) ||
                e.Email.Contains(search) ||
                e.Specialization.Contains(search));
        }

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
           }).FirstOrDefaultAsync();

        var teacherCourses = employeeRepository
            .SelectAllAsQueryable()
            .GroupJoin(
                courseRepository.SelectAllAsQueryable()
                .Include(c => c.Subject),
                t => t.Id,
                c => c.TeacherId,
                (teacher, courses) => new
                {
                    Id = teacher.Id,
                    Courses = courses.Select(c => new TeacherViewModel.CourseInfo
                    {
                        Id = c.Id,
                        Name = c.Name,
                        SubjectId = c.Subject.Id,
                        SubjectName = c.Subject.Name
                    })
                }).FirstOrDefaultAsync(result => result.Id == id);

        teacher.Courses = teacherCourses.Result.Courses.ToList();

        return teacher;
    }

    public Task<List<TeacherViewModel>> GetAllTeachersAsync(int companyId, string search, int? subjectId)
    {
        throw new NotImplementedException();
    }

    // ushbu method bajarilishi kerak
    //public async Task<List<TeacherViewModel>> GetAllTeachersAsync(int companyId, string search, int? subjectId)
    //{
    //    var query = employeeRepository
    //       .SelectAllAsQueryable()
    //       .Where(c => !c.IsDeleted && c.CompanyId == companyId && c.Role.Name.ToLower() == "teacher");

    //    if (!string.IsNullOrWhiteSpace(search))
    //    {
    //        search = search.ToLower();
    //        query = query.Where(t =>
    //            t.FirstName.ToLower().Contains(search) ||
    //            t.LastName.ToLower().Contains(search) ||
    //            t.Specialization.ToLower().Contains(search) ||
    //            t.Phone.Contains(search) ||
    //            t.Email.Contains(search));
    //    }

    //    if (subjectId != null)
    //        query = query.Where(t => t.SubjectId == subjectId);

    //    var subjects = await teacherSubjectRepository
    //       .SelectAllAsQueryable()
    //       .Include(ts => ts.Subject)
    //       .Join(
    //           employeeRepository.SelectAllAsQueryable(),
    //           tsubject => tsubject.TeacherId,
    //           teacher => teacher.Id,
    //           (ts, t) => new TeacherViewModel.SubjectInfo
    //           {
    //               Id = ts.Id,
    //               Name = ts.Subject.Name
    //           }).ToListAsync();

    //    var teacherCourses = employeeRepository
    //        .SelectAllAsQueryable()
    //        .Where(emp => emp.CompanyId == companyId && !emp.IsDeleted)
    //        .GroupJoin(
    //            courseRepository.SelectAllAsQueryable()
    //            .Include(c => c.Subject),
    //            t => t.Id,
    //            c => c.TeacherId,
    //            (teacher, courses) => new
    //            {
    //                Id = teacher.Id,
    //                Courses = courses.Select(c => new TeacherViewModel.CourseInfo
    //                {
    //                    Id = c.Id,
    //                    Name = c.Name,
    //                    SubjectId = c.Subject.Id,
    //                    SubjectName = c.Subject.Name
    //                })
    //            }).ToListAsync();
    //}
}
