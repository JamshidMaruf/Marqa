using System.Data;
using System.Diagnostics;
using FluentValidation;
using Marqa.DataAccess.UnitOfWork;
using Marqa.Domain.Entities;
using Marqa.Domain.Enums;
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
    IValidator<TeacherCreateModel> validatorTeacherCreate,
    IValidator<TeacherUpdateModel> validatorTeacherUpdate) : ITeacherService
{
    public async Task CreateAsync(TeacherCreateModel model)
    {
        await validatorTeacherCreate.EnsureValidatedAsync(model);
        var alreadyExistTeacher =
            await unitOfWork.Teachers.CheckExistAsync(t =>
                t.User.Phone == model.Phone && t.User.CompanyId == model.CompanyId);

        if (alreadyExistTeacher)
            throw new AlreadyExistException($"Teacher with this phone {model.Phone} already exists");

        var teacherPhone = model.Phone.TrimPhoneNumber();
        if (!teacherPhone.IsSuccessful)
            throw new ArgumentIsNotValidException("Invalid phone number!");

        var teacher = unitOfWork.Teachers.Insert(new Teacher
        {
            User = new User()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Phone = teacherPhone.Phone,
                Email = model.Email,
                PasswordHash = PasswordHelper.Hash(model.Password),
                Role = UserRole.Employee,
                CompanyId = model.CompanyId,
            },
            DateOfBirth = model.DateOfBirth,
            Gender = model.Gender,
            JoiningDate = model.JoiningDate,
            Qualification = model.Qualiification,
            Info = model.Info,
            Type = model.Type,
            Status = model.Status,
            PaymentType = model.PaymentType,
            FixSalary = TeacherPaymentType.Fixed == model.PaymentType ? model.Amount : 0,
            SalaryPercentPerStudent = TeacherPaymentType.Percentage == model.PaymentType ? model.Amount : 0,
            SalaryAmountPerHour = TeacherPaymentType.Hourly == model.PaymentType ? model.Amount : 0,
        });
        
        await subjectService.BulkAttachAsync(teacher.Id, model.SubjectIds);

        await unitOfWork.SaveAsync();
    }

    public async Task UpdateAsync(int id, TeacherUpdateModel model)
    {
        await validatorTeacherUpdate.EnsureValidatedAsync(model);
        var existTeacher = await unitOfWork.Teachers.SelectAsync(t => t.Id == id, includes: "User")
            ?? throw new NotFoundException($"Teacher was not found with ID = {id}");
        var existPhone = await unitOfWork.Teachers.SelectAsync(e =>
            e.User.Phone == model.Phone &&
            e.User.CompanyId == existTeacher.User.CompanyId &&
            e.Id != id, includes: "User");
        
        if (existPhone != null)
            throw new AlreadyExistException($"Employee with this phone {model.Phone} already exists");
        
        
        var teacherPhone = model.Phone.TrimPhoneNumber();
        if (!teacherPhone.IsSuccessful)
            throw new ArgumentIsNotValidException("Invalid phone number!");
        
        existTeacher.User.FirstName = model.FirstName;
        existTeacher.User.LastName = model.LastName;
        existTeacher.User.Email = model.Email;
        existTeacher.User.Phone = model.Phone;
        existTeacher.DateOfBirth = model.DateOfBirth;
        existTeacher.Qualification = model.Qualification;
        existTeacher.Info = model.Info;
        existTeacher.Gender = model.Gender;
        existTeacher.JoiningDate = model.JoiningDate;
        existTeacher.PaymentType = model.PaymentType;
        existTeacher.Status = model.Status;
        existTeacher.Type = model.Type;
        existTeacher.FixSalary = TeacherPaymentType.Fixed == model.PaymentType ? model.Amount : 0;
        existTeacher.SalaryAmountPerHour = TeacherPaymentType.Hourly == model.PaymentType ? model.Amount : 0;
        existTeacher.SalaryPercentPerStudent = TeacherPaymentType.Percentage == model.PaymentType ? model.Amount : 0;
        
        await subjectService.BulkAttachAsync(existTeacher.Id, model.SubjectIds);
        await unitOfWork.SaveAsync();   
    }

    public async Task DeleteAsync(int id)
    {
        var teacherForDeletion = await unitOfWork.Teachers
            .SelectAllAsQueryable(b => !b.IsDeleted,
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

        unitOfWork.Teachers.MarkAsDeleted(teacherForDeletion);

        await unitOfWork.SaveAsync();
    }

    // works but can be optimized
    public async Task<TeacherViewModel> GetAsync(int id)
    {
        var teacher = await unitOfWork.TeacherSubjects
           .SelectAllAsQueryable(ts => ts.TeacherId == id && !ts.IsDeleted,
           includes: ["Teacher", "Teacher.User"])
           .Select(ts => new TeacherViewModel
           {
               Id = ts.Teacher.Id,
               DateOfBirth = ts.Teacher.DateOfBirth,
               Gender = new TeacherViewModel.GenderInfo
               {
                   Id = Convert.ToInt32(ts.Teacher.Gender),
                   Name = Enum.GetName(ts.Teacher.Gender),
               },
               FirstName = ts.Teacher.User.FirstName,
               LastName = ts.Teacher.User.LastName,
               Email = ts.Teacher.User.Email,
               Phone = ts.Teacher.User.Phone,
               Qualification = ts.Teacher.Qualification,
               Status = new TeacherViewModel.StatusInfo
               {
                   Id = Convert.ToInt32(ts.Teacher.Status),
                   Name = Enum.GetName(ts.Teacher.Status),
               },
               TypeInfo = new TeacherViewModel.TeacherTypeInfo
               {
                   Id =  Convert.ToInt32(ts.Teacher.Type),
                   Type = Enum.GetName(ts.Teacher.Type),
               },
               Payment = new TeacherViewModel.TeacherPayment
               {
                   Id = Convert.ToInt32(ts.Teacher.PaymentType),
                   Type = ts.Teacher.PaymentType,
                   Name =  Enum.GetName(ts.Teacher.PaymentType),
                   FixSalary = TeacherPaymentType.Fixed == ts.Teacher.PaymentType ? ts.Teacher.FixSalary : 0,
                   SalaryAmountPerHour = TeacherPaymentType.Hourly == ts.Teacher.PaymentType ? ts.Teacher.SalaryAmountPerHour : 0,
                   SalaryPercentPerStudent = TeacherPaymentType.Percentage == ts.Teacher.PaymentType ? ts.Teacher.SalaryPercentPerStudent : 0,
               },
               JoiningDate = ts.Teacher.JoiningDate,
               Info = ts.Teacher.Info, 
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
           // .Where(c => c.TeacherId == id)
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
            includes: ["Teacher", "Teacher.User"])
            .Select(ts => new TeacherUpdateViewModel
            {
                Id = ts.Teacher.Id,
                DateOfBirth = ts.Teacher.DateOfBirth,
                Gender = new TeacherUpdateViewModel.GenderInfo
                {
                    Id = Convert.ToInt32(ts.Teacher.Gender),
                    Name = Enum.GetName(ts.Teacher.Gender),
                },
                FirstName = ts.Teacher.User.FirstName,
                LastName = ts.Teacher.User.LastName,
                Email = ts.Teacher.User.Email,
                Phone = ts.Teacher.User.Phone,
                Qualification =  ts.Teacher.Qualification,
                Status = new TeacherUpdateViewModel.StatusInfo
                {
                    Id = Convert.ToInt32(ts.Teacher.Status),
                    Name = Enum.GetName(ts.Teacher.Status),
                },
                TypeInfo = new TeacherUpdateViewModel.TeacherTypeInfo
                {
                    Id =  Convert.ToInt32(ts.Teacher.Type),
                    Type = Enum.GetName(ts.Teacher.Type),
                },
                Payment = new TeacherUpdateViewModel.TeacherPayment
                {
                    Id = Convert.ToInt32(ts.Teacher.PaymentType),
                    Type = ts.Teacher.PaymentType,
                    Name =  Enum.GetName(ts.Teacher.PaymentType),
                    FixSalary = TeacherPaymentType.Fixed == ts.Teacher.PaymentType ? ts.Teacher.FixSalary : 0,
                    SalaryAmountPerHour = TeacherPaymentType.Hourly == ts.Teacher.PaymentType ? ts.Teacher.SalaryAmountPerHour : 0,
                    SalaryPercentPerStudent = TeacherPaymentType.Percentage == ts.Teacher.PaymentType ? ts.Teacher.SalaryPercentPerStudent : 0,
                },
                JoiningDate = ts.Teacher.JoiningDate,
                Info = ts.Teacher.Info,
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

        return teacher;
    }

    // works but not optimal enough for large dataset
    public async Task<List<TeacherTableViewModel>> GetAllAsync(int companyId, string search = null, int? subjectId = null)
    {
        var teacherQuery = unitOfWork.Teachers
            .SelectAllAsQueryable(t => !t.IsDeleted && t.User.CompanyId == companyId && t.Type == TeacherType.Lead,
            includes: ["User", "Courses", "TeacherSubjects"]);

        if (!string.IsNullOrWhiteSpace(search))
        {
            search = search.ToLower().Trim();
            teacherQuery = teacherQuery.Where(t =>
                t.User.FirstName.ToLower().Contains(search) ||
                t.User.LastName.ToLower().Contains(search) ||
                t.User.Phone.Contains(search) ||
                t.User.Email.Contains(search));
        }

        var teachers = await teacherQuery.Select(t => new TeacherTableViewModel
        {
            Id =  t.Id,
            FirstName = t.User.FirstName,
            LastName = t.User.LastName,
            Phone = t.User.Phone,
            Gender =  new TeacherTableViewModel.GenderInfo
            {
                Id = Convert.ToInt32(t.Gender),
                Name = Enum.GetName(t.Gender),
            },
            Status = new TeacherTableViewModel.StatusInfo
            {
                Id =  Convert.ToInt32(t.Status),
                Name = Enum.GetName(t.Status),
            },
            Subjects = t.TeacherSubjects.Select(ts => new TeacherTableViewModel.SubjectInfo
            {
                Id = ts.SubjectId,
                Name = ts.Subject.Name
            }),
            Courses = t.Courses.Select( c => new TeacherTableViewModel.CourseInfo
            {
                Id =  c.Id,
                Name = c.Name,
                SubjectId = c.SubjectId,
                SubjectName = c.Subject.Name
            })
        }).ToListAsync();
        
        return teachers;
    }

    public async Task<CalculatedTeacherSalaryModel> CalculateTeacherSalaryAsync(int teacherId, int year, Month month)
    {
        //var teacherSalary = await unitOfWork.TeacherSalaries.SelectAsync(t => t.TeacherId == teacherId)
        //    ?? throw new NotFoundException($"No teacher was found with ID {teacherId}");

        //var teacher = await unitOfWork.Teachers.SelectAllAsQueryable(t =>
        //        t.Id == teacherId)
        //    .Include(t => t.Courses.Where(c =>
        //    c.StartDate.Year == year &&)
        //    .FirstOrDefaultAsync();

        //var paymentType = teacherSalary.PaymentType;
        //var studentCount = teacher.Courses.Select(c => c.Enrollments.Count).Sum();
        //var currentCourses = teacher.Courses.Where(c => c.Status == CourseStatus.Active).Count();
        //var calculatedModel = new CalculatedTeacherSalaryModel();

        //if (paymentType == TeacherPaymentType.Fixed)
        //{
        //    calculatedModel.StudentsCount = studentCount;
        //    calculatedModel.GroupsCount = teacher.Courses.Count;
        //    calculatedModel.Total = teacherSalary.Amount;

        //}
        //else if(paymentType ==TeacherPaymentType.Percentage)
        //{
        //    calculatedModel.StudentsCount = studentCount;
        //    calculatedModel.GroupsCount = teacher.Courses.Count;
        //    calculatedModel.Percent = teacherSalary.Percent;
        //    calculatedModel.Total = studentCount * currentCourses;
        //}

        return new CalculatedTeacherSalaryModel();
    }

    #region lab

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
    #endregion
}
