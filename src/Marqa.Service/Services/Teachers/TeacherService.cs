using FluentValidation;
using Marqa.Domain.Entities;
using Marqa.Domain.Enums;
using Marqa.Service.Exceptions;
using Marqa.Service.Extensions;
using Marqa.Service.Helpers;
using Marqa.Service.Services.Enums;
using Marqa.Service.Services.Subjects;
using Marqa.Service.Services.Teachers.Models;
using Microsoft.EntityFrameworkCore;

namespace Marqa.Service.Services.Teachers;

public class TeacherService(
    IUnitOfWork unitOfWork,
    ISubjectService subjectService,
    IValidator<TeacherCreateModel> validatorTeacherCreate,
    IValidator<TeacherUpdateModel> validatorTeacherUpdate,
    IEnumService enumService) : ITeacherService
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

        var transaction = await unitOfWork.BeginTransactionAsync();

        try
        {
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
                Qualification = model.Qualification,
                Info = model.Info,
                Type = model.Type,
                Status = model.Status,
                PaymentType = model.PaymentType,
                FixSalary = TeacherPaymentType.Fixed == model.PaymentType ? model.Amount : 0,
                SalaryPercentPerStudent = TeacherPaymentType.Percentage == model.PaymentType ? model.Amount : 0,
                SalaryAmountPerHour = TeacherPaymentType.Hourly == model.PaymentType ? model.Amount : 0,
            });

            await unitOfWork.SaveAsync();

            await subjectService.BulkAttachAsync(teacher.Id, model.SubjectIds);

            await unitOfWork.SaveAsync();
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
        var teacherForDeletion = await unitOfWork.Employees
            .SelectAllAsQueryable(e => e.Id == id && !e.IsDeleted,
                includes: "User")
            .FirstOrDefaultAsync()
            ?? throw new NotFoundException($"Teacher was not found with ID = {id}");

        bool hasActiveCourses = await unitOfWork.CourseTeachers
            .SelectAllAsQueryable(ct => ct.TeacherId == id && !ct.IsDeleted,
                includes: "Course")
            .AnyAsync(ct => !ct.Course.IsDeleted && ct.Course.Status == CourseStatus.Active);

        if (hasActiveCourses)
        {
            var activeCourses = await unitOfWork.CourseTeachers
                .SelectAllAsQueryable(ct => ct.TeacherId == id && !ct.IsDeleted,
                    includes: "Course")
                .Where(ct => !ct.Course.IsDeleted && ct.Course.Status == CourseStatus.Active)
                .Select(ct => ct.Course.Name)
                .ToListAsync();

            var teacherName = $"{teacherForDeletion.User.FirstName} {teacherForDeletion.User.LastName}";

            throw new InvalidOperationException(
                $"Cannot delete teacher '{teacherName}'. " +
                $"This teacher has {activeCourses.Count} active course(s): {string.Join(", ", activeCourses)}. " +
                "Please reassign courses to another teacher or deactivate courses first.");
        }

        var courseTeachers = await unitOfWork.CourseTeachers
            .SelectAllAsQueryable(ct => ct.TeacherId == id && !ct.IsDeleted)
            .ToListAsync();

        foreach (var courseTeacher in courseTeachers)
        {
            unitOfWork.CourseTeachers.MarkAsDeleted(courseTeacher);
        }

        var teacherSubjects = await unitOfWork.TeacherSubjects
            .SelectAllAsQueryable(ts => ts.TeacherId == id && !ts.IsDeleted)
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
        var teacher = await unitOfWork.Teachers
           .SelectAllAsQueryable(ts => ts.Id == id)
           .Select(ts => new TeacherViewModel
           {
               Id = ts.Id,
               DateOfBirth = ts.DateOfBirth,
               Gender = new TeacherViewModel.GenderInfo
               {
                   Id = Convert.ToInt32(ts.Gender),
                   Name = Enum.GetName(ts.Gender),
               },
               FirstName = ts.User.FirstName,
               LastName = ts.User.LastName,
               Email = ts.User.Email,
               Phone = ts.User.Phone,
               Qualification = ts.Qualification,
               Status = new TeacherViewModel.StatusInfo
               {
                   Id = Convert.ToInt32(ts.Status),
                   Name = Enum.GetName(ts.Status),
               },
               TypeInfo = new TeacherViewModel.TeacherTypeInfo
               {
                   Id = Convert.ToInt32(ts.Type),
                   Type = Enum.GetName(ts.Type),
               },
               Payment = new TeacherViewModel.TeacherPayment
               {
                   Id = Convert.ToInt32(ts.PaymentType),
                   Type = ts.PaymentType,
                   Name = Enum.GetName(ts.PaymentType),
                   FixSalary = TeacherPaymentType.Fixed == ts.PaymentType ? ts.FixSalary : 0,
                   SalaryAmountPerHour = TeacherPaymentType.Hourly == ts.PaymentType ? ts.SalaryAmountPerHour : 0,
                   SalaryPercentPerStudent = TeacherPaymentType.Percentage == ts.PaymentType ? ts.SalaryPercentPerStudent : 0,
               },
               JoiningDate = ts.JoiningDate,
               Info = ts.Info,
               Subjects = ts.TeacherSubjects.Select(ts => new TeacherViewModel.SubjectInfo
               {
                   Id = ts.SubjectId,
                   Name = ts.Subject.Name
               }),
               Courses = ts.Courses.Select(c => new TeacherViewModel.CourseInfo
               {
                   Id = c.Id,
                   Name = c.Name,
                   SubjectId = c.Subject.Id,
                   SubjectName = c.Subject.Name
               })
           })
           .FirstOrDefaultAsync()
            ?? throw new NotFoundException($"No teacher was found with ID = {id}.");

        return teacher;
    }

    public async Task<TeacherUpdateViewModel> GetForUpdateAsync(int id)
    {
        var teacher = await unitOfWork.Teachers
            .SelectAllAsQueryable(ts => ts.Id == id)
            .Select(ts => new TeacherUpdateViewModel
            {
                Id = ts.Id,
                DateOfBirth = ts.DateOfBirth,
                Gender = new TeacherUpdateViewModel.GenderInfo
                {
                    Id = Convert.ToInt32(ts.Gender),
                    Name = Enum.GetName(ts.Gender),
                },
                FirstName = ts.User.FirstName,
                LastName = ts.User.LastName,
                Email = ts.User.Email,
                Phone = ts.User.Phone,
                Qualification = ts.Qualification,
                Status = new TeacherUpdateViewModel.StatusInfo
                {
                    Id = Convert.ToInt32(ts.Status),
                    Name = Enum.GetName(ts.Status),
                },
                TypeInfo = new TeacherUpdateViewModel.TeacherTypeInfo
                {
                    Id = Convert.ToInt32(ts.Type),
                    Type = Enum.GetName(ts.Type),
                },
                Payment = new TeacherUpdateViewModel.TeacherPayment
                {
                    Id = Convert.ToInt32(ts.PaymentType),
                    Type = ts.PaymentType,
                    Name = Enum.GetName(ts.PaymentType),
                    FixSalary = TeacherPaymentType.Fixed == ts.PaymentType ? ts.FixSalary : 0,
                    SalaryAmountPerHour = TeacherPaymentType.Hourly == ts.PaymentType ? ts.SalaryAmountPerHour : 0,
                    SalaryPercentPerStudent = TeacherPaymentType.Percentage == ts.PaymentType ? ts.SalaryPercentPerStudent : 0,
                },
                JoiningDate = ts.JoiningDate,
                Info = ts.Info,
                Subjects = ts.TeacherSubjects.Select(ts => new TeacherUpdateViewModel.SubjectInfo
                {
                    Id = ts.SubjectId,
                    Name = ts.Subject.Name
                })
            })
            .FirstOrDefaultAsync()
             ?? throw new NotFoundException($"No teacher was found with ID = {id}");

        return teacher;
    }

    public async Task<List<TeacherTableViewModel>> GetAllAsync(int companyId, string search = null, int? subjectId = null)
    {
        var teacherQuery = unitOfWork.Teachers
            .SelectAllAsQueryable(t => t.User.CompanyId == companyId && t.Type == TeacherType.Lead,
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
            Id = t.Id,
            FirstName = t.User.FirstName,
            LastName = t.User.LastName,
            Phone = t.User.Phone,
            Gender = new TeacherTableViewModel.GenderInfo
            {
                Id = Convert.ToInt32(t.Gender),
                Name = Enum.GetName(t.Gender),
            },
            Status = new TeacherTableViewModel.StatusInfo
            {
                Id = Convert.ToInt32(t.Status),
                Name = Enum.GetName(t.Status),
            },
            Subjects = t.TeacherSubjects.Select(ts => new TeacherTableViewModel.SubjectInfo
            {
                Id = ts.SubjectId,
                Name = ts.Subject.Name
            }),
            Courses = t.Courses.Select(c => new TeacherTableViewModel.CourseInfo
            {
                Id = c.Id,
                Name = c.Name,
                SubjectId = c.SubjectId,
                SubjectName = c.Subject.Name
            })
        }).ToListAsync();

        return teachers;
    }

    public async Task<CalculatedTeacherSalaryModel> CalculateTeacherSalaryAsync(int teacherId, int year, Month month)
    {
        var teacher = await unitOfWork.Teachers.SelectAsync(t => t.Id == teacherId)
            ?? throw new NotFoundException($"No teacher was found with ID {teacherId}");

        var query = unitOfWork.LessonAttendances
            .SelectAllAsQueryable(
                predicate: la => la.Lesson.Course.CourseTeachers.Any(ct => ct.TeacherId == teacherId) &&
                                 la.Lesson.Date.Year == year &&
                                 la.Lesson.Date.Month == (int)month,
                includes: ["Lesson", "Lesson.Course", "Lesson.Course.CourseTeachers"]);

        var activeCourses = await query.Select(la => la.Lesson.Course).Distinct().ToListAsync();
        
        var activeStudentsCount = activeCourses.Sum(c => c.StudentCount);

        var result = new CalculatedTeacherSalaryModel();
        
        result.GroupsCount = activeCourses.Count;
        result.ActiveStudentsCount = activeStudentsCount;

        if (teacher.PaymentType == TeacherPaymentType.Fixed)
        {
            foreach (var course in activeCourses)
            {
                result.FixedSalaries.Add(new CalculatedTeacherSalaryModel.FixedSalary
                {
                    CourseId = course.Id,
                    CourseName = course.Name,
                    ActiveStudentsCount = course.StudentCount,
                    FixSalary = Convert.ToDecimal(teacher.FixSalary)
                });
                
                result.TotalSalary += Convert.ToDecimal(teacher.FixSalary);
            }
        }
        else if (teacher.PaymentType == TeacherPaymentType.Percentage)
        {
            foreach (var course in activeCourses)
            {
                result.PercentageSalaries.Add(new CalculatedTeacherSalaryModel.PercentageSalary
                {
                    CourseId = course.Id,
                    CourseName = course.Name,
                    ActiveStudentsCount = course.StudentCount,
                    Percent = Convert.ToDecimal(teacher.SalaryPercentPerStudent),
                    Total = Convert.ToDecimal(((course.StudentCount * course.Price) / 100) * teacher.SalaryPercentPerStudent)
                });
                
                result.TotalSalary += Convert.ToDecimal(((course.StudentCount * course.Price) / 100) * teacher.SalaryPercentPerStudent);
            }
        }
        else if (teacher.PaymentType == TeacherPaymentType.Hourly)
        {
            var attendedLessons = unitOfWork.Lessons
                .SelectAllAsQueryable(l =>
                    l.TeacherId == teacherId &&
                    l.Date.Year == year &&
                    l.Date.Month == (int)month &&
                    l.IsAttended)
                .ToListAsync();

            double attendedLessonInHours = 0d;

            // foreach (var lesson in await attendedLessons)
            // {
            //     attendedLessons = lesson.StartTime.Hour - lesson.EndTime.Hour;
            // }
            
            foreach (var course in activeCourses)
            {
                result.HourlySalaries.Add(new CalculatedTeacherSalaryModel.HourlySalary
                {
                    CourseId = course.Id,
                    CourseName = course.Name,
                    ActiveStudentsCount = course.StudentCount,
                    Hours = attendedLessonInHours,
                    Amount = Convert.ToDecimal(teacher.SalaryAmountPerHour),
                    Total = Convert.ToDecimal((decimal)attendedLessonInHours * teacher.SalaryAmountPerHour)
                });
                
                result.TotalSalary += Convert.ToDecimal((decimal)attendedLessonInHours * teacher.SalaryAmountPerHour);
            }
            
        }
        else if (teacher.PaymentType == TeacherPaymentType.Mixed)
        {
            foreach (var course in activeCourses)
            {
                result.MixedSalaries.Add(new CalculatedTeacherSalaryModel.MixedSalary
                {
                    CourseId = course.Id,
                    CourseName = course.Name,
                    ActiveStudentsCount = course.StudentCount,
                    FixSalary = Convert.ToDecimal(teacher.FixSalary),
                    Percent = Convert.ToInt32(teacher.SalaryPercentPerStudent),
                    Total = Convert.ToDecimal(teacher.FixSalary) + Convert.ToDecimal(((course.StudentCount * course.Price) / 100) * teacher.SalaryPercentPerStudent)
                });
                
                result.TotalSalary += Convert.ToDecimal(teacher.FixSalary) + Convert.ToDecimal(((course.StudentCount * course.Price) / 100) * teacher.SalaryPercentPerStudent);
            }
        }
        
        return result;
    }

    public async Task<List<TeacherPaymentGetModel>> GetTeacherPaymentTypes()
    {
        var enumValues = enumService.GetEnumValues<TeacherPaymentType>();
        var paymentTypes = enumValues.Select(ev => new TeacherPaymentGetModel
        {
            Id = ev.Id,
            Name = ev.Name
        }).ToList();

        return paymentTypes;
    }

    public Task<TeacherStatistics> GetTeacherStatisticsAsync(int teacherId)
    {
        throw new NotImplementedException();
    }
}
