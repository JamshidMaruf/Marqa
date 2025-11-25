using System.Data;
using FluentValidation;
using Marqa.DataAccess.UnitOfWork;
using Marqa.Service.Exceptions;
using Marqa.Service.Services.Employees;
using Marqa.Service.Services.Employees.Models;
using Marqa.Service.Services.Subjects;
using Marqa.Service.Services.Teachers.Models;
using Microsoft.EntityFrameworkCore;
using static Marqa.Service.Services.Teachers.Models.TeacherViewModel;

namespace Marqa.Service.Services.Teachers;

public class TeacherService(
    IUnitOfWork unitOfWork,
    ISubjectService subjectService,
    IEmployeeService employeeService) : ITeacherService
{
    public async Task CreateAsync(TeacherCreateModel model)
    {
        var transaction = await unitOfWork.BeginTransactionAsync();

        try
        {
            var teacher = await employeeService.CreateAsync(new EmployeeCreateModel
            {
                CompanyId = model.CompanyId,
                FirstName = model.FirstName,
                LastName = model.LastName,
                DateOfBirth = model.DateOfBirth,
                Gender = model.Gender,
                Phone = model.Phone,
                Email = model.Email,
                Status = model.Status,
                Password = model.Password,
                JoiningDate = model.JoiningDate,
                Specialization = model.Specialization,
                Info = model.Info,
                RoleId = model.RoleId
            });

            await subjectService.BulkAttachAsync(teacher.Id, model.SubjectIds);

            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task UpdateAsync(int id, TeacherUpdateModel model)
    {
        var transaction = await unitOfWork.BeginTransactionAsync();

        try
        {
            await employeeService.UpdateAsync(id, new EmployeeUpdateModel
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                DateOfBirth = model.DateOfBirth,
                Gender = model.Gender,
                Phone = model.Phone,
                Email = model.Email,
                Status = model.Status,
                JoiningDate = model.JoiningDate,
                Specialization = model.Specialization,
                RoleId = model.RoleId
            });

            await subjectService.BulkAttachAsync(id, model.SubjectIds);
            await unitOfWork.SaveAsync();

            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task DeleteAsync(int id)
    {
        var teacherForDeletion = await unitOfWork.Employees
            .SelectAllAsQueryable(b => !b.IsDeleted && b.Role.CanTeach,
            includes: "Role")
            .FirstOrDefaultAsync()
            ?? throw new NotFoundException($"Teacher was not found with ID = {id}");

        var teacherSubjects = await unitOfWork.TeacherSubjects
            .SelectAllAsQueryable(ts => ts.TeacherId == id)
            .ToListAsync();

        foreach (var teacherSubject in teacherSubjects)
        {
            unitOfWork.TeacherSubjects.MarkAsDeleted(teacherSubject);
        }

        unitOfWork.Employees.MarkAsDeleted(teacherForDeletion);

        await unitOfWork.SaveAsync();
    }

    // works but can be optimized
    public async Task<TeacherViewModel> GetAsync(int id)
    {
        var teacher = await unitOfWork.TeacherSubjects
           .SelectAllAsQueryable(ts => ts.TeacherId == id && !ts.IsDeleted,
           includes: ["Teacher"])
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
               Specialization = ts.Teacher.Specialization
           })
           .FirstOrDefaultAsync()
            ?? throw new NotFoundException($"No teacher was found with ID = {id}.");

        var subjects = await unitOfWork.TeacherSubjects
            .SelectAllAsQueryable(ts => ts.TeacherId == id)
            .Select(ts => new TeacherViewModel.SubjectInfo
            {
                Id = ts.SubjectId,
                Name = ts.Subject.Name
            })
            .ToListAsync();

        teacher.Subjects = subjects;

        var courses = await unitOfWork.Courses.SelectAllAsQueryable(ts => !ts.IsDeleted,
            includes: "Subject")
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

    // works but can be optimized 
    public async Task<TeacherUpdateViewModel> GetForUpdateAsync(int id)
    {
        var teacher = await unitOfWork.TeacherSubjects
           .SelectAllAsQueryable(ts => ts.TeacherId == id && !ts.IsDeleted,
           includes: ["Teacher"])
           .Select(ts => new TeacherUpdateViewModel
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
               Specialization = ts.Teacher.Specialization
           })
           .FirstOrDefaultAsync()
            ?? throw new NotFoundException($"No teacher was found with ID = {id}.");

        var subjects = await unitOfWork.TeacherSubjects
            .SelectAllAsQueryable(ts => ts.TeacherId == id)
            .Select(ts => new TeacherUpdateViewModel.SubjectInfo
            {
                Id = ts.SubjectId,
                Name = ts.Subject.Name
            })
            .ToListAsync();

        teacher.Subjects = subjects;

        var courses = await unitOfWork.Courses.SelectAllAsQueryable(ts => !ts.IsDeleted,
            includes: "Subject")
            .Where(c => c.TeacherId == id)
            .Select(c => new TeacherUpdateViewModel.CourseInfo
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

    // works but not optimal enough for large dataset
    public async Task<List<TeacherViewModel>> GetAllAsync(int companyId, string search = null, int? subjectId = null)
    {
        var teacherQuery = unitOfWork.Employees
            .SelectAllAsQueryable(t => !t.IsDeleted && t.CompanyId == companyId,
            includes: ["Role"]);

        if (!string.IsNullOrWhiteSpace(search))
        {
            search = search.ToLower().Trim();
            teacherQuery = teacherQuery.Where(t =>
                t.FirstName.ToLower().Contains(search) ||
                t.LastName.ToLower().Contains(search) ||
                t.Specialization.ToLower().Contains(search) ||
                t.Phone.Contains(search) ||
                t.Email.Contains(search));
        }
        
        var teacherWithoutCourses = await teacherQuery.GroupJoin(
                unitOfWork.TeacherSubjects.SelectAllAsQueryable(t => !t.IsDeleted, includes: new[] { "Subject" }),
                t => t.Id,
                ts => ts.TeacherId,
                (t, ts) => new TeacherViewModel
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
                    Subjects = ts.Select(t => new SubjectInfo
                    {
                        Id = t.Id,
                        Name = t.Subject.Name,
                    }),
                    Role = new RoleInfo
                    {
                        Id = t.Role.Id,
                        Name = t.Role.Name
                    }
                }).ToListAsync();

        if (subjectId is not null)
            teacherWithoutCourses = teacherWithoutCourses.Where(t => t.Subjects.Select(t => t.Id).ToList().Contains(subjectId.Value)).ToList();

        var teachers = teacherWithoutCourses.GroupJoin(
                await unitOfWork.Courses.SelectAllAsQueryable(t => !t.IsDeleted, includes: new[] { "Subject" }).ToListAsync(),
                t => t.Id,
                c => c.TeacherId,
                (t, courses) => new TeacherViewModel
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
                    Subjects = t.Subjects,
                    Courses = courses.Select(c => new CourseInfo
                    {
                        Id = c.Id,
                        Name = c.Name,
                        SubjectId = c.Subject.Id,
                        SubjectName = c.Subject.Name
                    }),
                    Role = t.Role
                }
            );

        return teachers.ToList();
    }

    //public async Task<List<TeacherViewModel>> GetAllAsync(int companyId, string search = null, int? subjectId = null)
    //{
    //    var teacherQuery = unitOfWork.Employees
    //        .SelectAllAsQueryable(t => !t.IsDeleted && t.CompanyId == companyId,
    //        includes: ["Role"])
    //        .GroupJoin(
    //            unitOfWork.TeacherSubjects.SelectAllAsQueryable(t => !t.IsDeleted, includes: new[] { "Subject" }),
    //            t => t.Id,
    //            ts => ts.TeacherId,
    //            (t, ts) => new
    //            {
    //                Id = t.Id,
    //                DateOfBirth = t.DateOfBirth,
    //                Gender = t.Gender,
    //                FirstName = t.FirstName,
    //                LastName = t.LastName,
    //                Email = t.Email,
    //                Phone = t.Phone,
    //                Status = t.Status,
    //                JoiningDate = t.JoiningDate,
    //                Specialization = t.Specialization,
    //                Subjects = ts,
    //                Role = t.Role
    //            });

    //    if (subjectId is not null)
    //        teacherQuery = teacherQuery.Where(t => t.Subjects.Select(t => t.Id).Contains(subjectId.Value));

    //    var teachers = await teacherQuery.GroupJoin(
    //             unitOfWork.Courses.SelectAllAsQueryable(t => !t.IsDeleted, includes: new[] { "Subject" }),
    //            t => t.Id,
    //            c => c.TeacherId,
    //            (t, courses) => new TeacherViewModel
    //            {
    //                Id = t.Id,
    //                DateOfBirth = t.DateOfBirth,
    //                Gender = t.Gender,
    //                FirstName = t.FirstName,
    //                LastName = t.LastName,
    //                Email = t.Email,
    //                Phone = t.Phone,
    //                Status = t.Status,
    //                JoiningDate = t.JoiningDate,
    //                Specialization = t.Specialization,
    //                Subjects = t.Subjects.Select(t => new SubjectInfo
    //                {
    //                    Id = t.Id,
    //                    Name = t.Subject.Name,
    //                }),
    //                Courses = courses.Select(c => new CourseInfo
    //                {
    //                    Id = c.Id,
    //                    Name = c.Name,
    //                    SubjectId = c.Subject.Id,
    //                    SubjectName = c.Subject.Name
    //                }),
    //                Role = new RoleInfo
    //                {
    //                    Id = t.Role.Id,
    //                    Name = t.Role.Name
    //                }
    //            }
    //        ).ToListAsync();

    //    return teachers;
    //}
}
