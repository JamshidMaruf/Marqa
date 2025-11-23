using System.Data;
using FluentValidation;
using Marqa.DataAccess.UnitOfWork;
using Marqa.Domain.Entities;
using Marqa.Service.Exceptions;
using Marqa.Service.Extensions;
using Marqa.Service.Helpers;
using Marqa.Service.Services.Employees;
using Marqa.Service.Services.Employees.Models;
using Marqa.Service.Services.Subjects;
using Marqa.Service.Services.Teachers.Models;
using Microsoft.EntityFrameworkCore;

namespace Marqa.Service.Services.Teachers;

public class TeacherService(
    IUnitOfWork unitOfWork,
    ISubjectService subjectService,
    IValidator<TeacherUpdateModel> validatorTeacherUpdate,
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

    public async Task<TeacherViewModel> GetAsync(int id)
    {
        var teacher = await unitOfWork.TeacherSubjects
           .SelectAllAsQueryable(ts => ts.TeacherId == id && !ts.IsDeleted,
           includes: ["Teacher", "Subject"])
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
                   Id = ts.SubjectId,
                   Name = ts.Subject.Name
               }
           })
           .FirstOrDefaultAsync()
            ?? throw new NotFoundException($"No teacher was found with ID = {id}.");

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

    public async Task<List<TeacherViewModel>> GetAllAsync(int companyId, string search = null, int? subjectId = null)
    {
        var teacherQuery = unitOfWork.TeacherSubjects
            .SelectAllAsQueryable(t => !t.IsDeleted,
            includes: ["Teacher", "Subject"])
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

        var teacherCourses = await teacherQuery
            .GroupJoin(
                unitOfWork.Courses.SelectAllAsQueryable(t => !t.IsDeleted, includes: new[] { "Subject" }),
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
                    Subject = t.Subject,
                    Courses = courses.Select(c => new TeacherViewModel.CourseInfo
                    {
                        Id = c.Id,
                        Name = c.Name,
                        SubjectId = c.Subject.Id,
                        SubjectName = c.Subject.Name
                    })
                }
            )
            .ToListAsync();

        return teacherCourses;
    }
}
