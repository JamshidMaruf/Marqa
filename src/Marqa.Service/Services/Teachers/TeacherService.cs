using FluentValidation;
using Marqa.Domain.Entities;
using Marqa.Domain.Enums;
using Marqa.Service.Exceptions;
using Marqa.Service.Extensions;
using Marqa.Service.Helpers;
using Marqa.Service.Services.Enums;
using Marqa.Service.Services.Teachers.Models;
using Marqa.Shared.Models;
using Marqa.Shared.Services;
using Microsoft.EntityFrameworkCore;

namespace Marqa.Service.Services.Teachers;

public class TeacherService(
    IUnitOfWork unitOfWork,
    IValidator<TeacherCreateModel> validatorTeacherCreate,
    IValidator<TeacherUpdateModel> validatorTeacherUpdate,
    IEnumService enumService,
    IPaginationService paginationService) : ITeacherService
{
    public async Task CreateAsync(TeacherCreateModel model)
    {
        await validatorTeacherCreate.EnsureValidatedAsync(model);

        var alreadyExistTeacher =
            await unitOfWork.Teachers.CheckExistAsync(t =>
                t.User.Phone == model.Phone && t.CompanyId == model.CompanyId);

        if (alreadyExistTeacher)
            throw new AlreadyExistException($"Teacher with this phone {model.Phone} already exists");

        var teacherPhone = model.Phone.TrimPhoneNumber();
        if (!teacherPhone.IsSuccessful)
            throw new ArgumentIsNotValidException("Invalid phone number!");

        var transaction = await unitOfWork.BeginTransactionAsync();

        try
        {
            var user = new User()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Phone = teacherPhone.Phone,
                Email = model.Email,
                PasswordHash = PasswordHelper.Hash(model.Password),
                Role = UserRole.Employee
            };

            unitOfWork.Users.Insert(user);

            await unitOfWork.SaveAsync();

            unitOfWork.Teachers.Insert(new Teacher
            {
                UserId = user.Id,
                CompanyId = model.CompanyId,
                DateOfBirth = model.DateOfBirth,
                Gender = model.Gender,
                JoiningDate = model.JoiningDate,
                Qualification = model.Qualification,
                Info = model.Info,
                Type = model.Type,
                Status = model.Status,
                PaymentType = model.PaymentType,
                FixSalary = TeacherSalaryType.Fixed == model.PaymentType || TeacherSalaryType.Mixed == model.PaymentType ? model.FixSalary : 0,
                SalaryPercentPerStudent = TeacherSalaryType.Percentage == model.PaymentType || TeacherSalaryType.Mixed == model.PaymentType ? model.SalaryPercentPerStudent : 0,
                SalaryAmountPerHour = TeacherSalaryType.Hourly == model.PaymentType ? model.SalaryAmountPerHour : 0
            });

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
            e.CompanyId == existTeacher.CompanyId &&
            e.Id != id);

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
        existTeacher.Status = model.Status;
        existTeacher.Type = model.Type;
        existTeacher.PaymentType = model.PaymentType;
        existTeacher.FixSalary = TeacherSalaryType.Fixed == model.PaymentType || TeacherSalaryType.Mixed == model.PaymentType ? model.FixSalary : 0;
        existTeacher.SalaryPercentPerStudent = TeacherSalaryType.Percentage == model.PaymentType || TeacherSalaryType.Mixed == model.PaymentType ? model.SalaryPercentPerStudent : 0;
        existTeacher.SalaryAmountPerHour = TeacherSalaryType.Hourly == model.PaymentType ? model.SalaryAmountPerHour : 0;

        unitOfWork.Teachers.Update(existTeacher);

        await unitOfWork.SaveAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var teacherForDeletion = await unitOfWork.Teachers
            .SelectAllAsQueryable(e => e.Id == id,
                includes: "User")
            .FirstOrDefaultAsync()
            ?? throw new NotFoundException($"Teacher was not found with ID = {id}");

        var activeCourses = await unitOfWork.CourseTeachers
            .SelectAllAsQueryable(ct => ct.TeacherId == id,
                includes: "Course")
            .Where(ct => ct.Course.Status == CourseStatus.Active &&
                         ct.Course.Status == CourseStatus.Upcoming)
            .Select(ct => ct.Course.Name)
            .ToListAsync();

        if (activeCourses.Count > 0)
        {
            var teacherName = $"{teacherForDeletion.User.FirstName} {teacherForDeletion.User.LastName}";

            throw new InvalidOperationException(
                $"Cannot delete teacher '{teacherName}'. " +
                $"This teacher has {activeCourses.Count} active or upcoming course(s): {string.Join(", ", activeCourses)}. " +
                "Please detach or reassign courses to another teacher first");
        }

        unitOfWork.Users.MarkAsDeleted(teacherForDeletion.User);

        unitOfWork.Teachers.MarkAsDeleted(teacherForDeletion);
        await unitOfWork.SaveAsync();
    }

    public async Task<TeacherViewModel> GetAsync(int id)
    {
        var teacher = await unitOfWork.Teachers
           .SelectAllAsQueryable(ts => ts.Id == id)
           .Select(t => new TeacherViewModel
           {
               Id = t.Id,
               DateOfBirth = t.DateOfBirth,
               Gender = new GenderInfo
               {
                   Id = Convert.ToInt32(t.Gender),
                   Name = enumService.GetEnumDescription(t.Gender),
               },
               FirstName = t.User.FirstName,
               LastName = t.User.LastName,
               Email = t.User.Email,
               Phone = t.User.Phone,
               Qualification = t.Qualification,
               Status = new StatusInfo
               {
                   Id = Convert.ToInt32(t.Status),
                   Name = enumService.GetEnumDescription(t.Status),
               },
               TypeInfo = new TeacherTypeInfo
               {
                   Id = Convert.ToInt32(t.Type),
                   Name = enumService.GetEnumDescription(t.Type),
               },
               Payment = new TeacherPayment
               {
                   Id = Convert.ToInt32(t.PaymentType),
                   Name = enumService.GetEnumDescription(t.PaymentType),
                   FixSalary = t.FixSalary,
                   SalaryAmountPerHour = t.SalaryAmountPerHour,
                   SalaryPercentPerStudent = t.SalaryPercentPerStudent,
               },
               JoiningDate = t.JoiningDate,
               Info = t.Info,
               Courses = t.TeacherCourses.Select(c => new CourseInfo
               {
                   Id = c.Course.Id,
                   Name = c.Course.Name
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
            .Select(t => new TeacherUpdateViewModel
            {
                Id = t.Id,
                DateOfBirth = t.DateOfBirth,
                Gender = new GenderInfo
                {
                    Id = Convert.ToInt32(t.Gender),
                    Name = enumService.GetEnumDescription(t.Gender),
                },
                FirstName = t.User.FirstName,
                LastName = t.User.LastName,
                Email = t.User.Email,
                Phone = t.User.Phone,
                Qualification = t.Qualification,
                Status = new StatusInfo
                {
                    Id = Convert.ToInt32(t.Status),
                    Name = enumService.GetEnumDescription(t.Status),
                },
                Type = new TeacherTypeInfo
                {
                    Id = Convert.ToInt32(t.Type),
                    Name = enumService.GetEnumDescription(t.Type),
                },
                Payment = new TeacherPayment
                {
                    Id = Convert.ToInt32(t.PaymentType),
                    Name = enumService.GetEnumDescription(t.PaymentType),
                    FixSalary = t.FixSalary,
                    SalaryAmountPerHour = t.SalaryAmountPerHour,
                    SalaryPercentPerStudent = t.SalaryPercentPerStudent,
                },
                JoiningDate = t.JoiningDate,
                Info = t.Info
            })
            .FirstOrDefaultAsync()
             ?? throw new NotFoundException($"No teacher was found with ID = {id}");

        return teacher;
    }

    public async Task<List<TeacherTableViewModel>> GetAllAsync(
        int companyId,
        PaginationParams @params,
        string search = null,
        TeacherStatus? status = null)
    {
        var teacherQuery = unitOfWork.Teachers
            .SelectAllAsQueryable(t => t.CompanyId == companyId,
            includes: ["User", "TeacherCourses", "TeacherCourses.Course"]);

        if (!string.IsNullOrWhiteSpace(search))
        {
            search = search.ToLower().Trim();
            teacherQuery = teacherQuery.Where(t =>
                t.User.FirstName.ToLower().Contains(search) ||
                t.User.LastName.ToLower().Contains(search) ||
                t.User.Phone.Contains(search) ||
                t.User.Email.Contains(search));
        }

        if (status != null && status != 0)
            teacherQuery = teacherQuery.Where(t => t.Status == status);

        var paginatedTeachers = await paginationService.Paginate(teacherQuery, @params).ToListAsync();

        var teachers = paginatedTeachers.Select(t => new TeacherTableViewModel
        {
            Id = t.Id,
            FirstName = t.User.FirstName,
            LastName = t.User.LastName,
            Phone = t.User.Phone,
            JoiningDate = t.JoiningDate,
            Qualification = t.Qualification,
            Status = new StatusInfo
            {
                Id = Convert.ToInt32(t.Status),
                Name = enumService.GetEnumDescription(t.Status),
            },
            Type = new TeacherTypeInfo
            {
                Id = Convert.ToInt32(t.Type),
                Name = enumService.GetEnumDescription(t.Type),
            },
            Courses = t.TeacherCourses.Select(c => new CourseInfo
            {
                Id = c.Course.Id,
                Name = c.Course.Name,
                Status = c.Course.Status.ToString()
            }).ToList()
        }).ToList();

        return teachers;
    }

    public async Task<List<TeacherMinimalListModel>> GetMinimalListAsync(int companyId)
    {
        return await unitOfWork.Teachers
            .SelectAllAsQueryable(t => t.CompanyId == companyId, includes: "User")
            .OrderBy(t => t.User.LastName)
            .ThenBy(t => t.User.FirstName)
            .Select(t => new TeacherMinimalListModel
            {
                Id = t.Id,
                FirstName = t.User.FirstName,
                LastName = t.User.LastName,
                TypeName = t.Type.ToString()
            })
            .ToListAsync();
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

        var activeStudentsCount = activeCourses.Sum(c => c.CurrentStudentCount);

        var result = new CalculatedTeacherSalaryModel();

        result.GroupsCount = activeCourses.Count;
        result.ActiveStudentsCount = activeStudentsCount;

        if (teacher.PaymentType == TeacherSalaryType.Fixed)
        {
            foreach (var course in activeCourses)
            {
                result.FixedSalaries.Add(new CalculatedTeacherSalaryModel.FixedSalary
                {
                    CourseId = course.Id,
                    CourseName = course.Name,
                    ActiveStudentsCount = course.CurrentStudentCount,
                    FixSalary = Convert.ToDecimal(teacher.FixSalary)
                });

                result.TotalSalary += Convert.ToDecimal(teacher.FixSalary);
            }
        }
        else if (teacher.PaymentType == TeacherSalaryType.Percentage)
        {
            foreach (var course in activeCourses)
            {
                result.PercentageSalaries.Add(new CalculatedTeacherSalaryModel.PercentageSalary
                {
                    CourseId = course.Id,
                    CourseName = course.Name,
                    ActiveStudentsCount = course.CurrentStudentCount,
                    Percent = Convert.ToDecimal(teacher.SalaryPercentPerStudent),
                    Total = Convert.ToDecimal(((course.CurrentStudentCount * course.Price) / 100) * teacher.SalaryPercentPerStudent)
                });

                result.TotalSalary += Convert.ToDecimal(((course.CurrentStudentCount * course.Price) / 100) * teacher.SalaryPercentPerStudent);
            }
        }
        else if (teacher.PaymentType == TeacherSalaryType.Hourly)
        {
            var attendedLessons = await unitOfWork.Lessons
                .SelectAllAsQueryable(l =>
                    l.TeacherId == teacherId &&
                    l.Date.Year == year &&
                    l.Date.Month == (int)month &&
                    l.IsAttended)
                .ToListAsync();

            double attendedLessonInHours = 0d;

            foreach (var lesson in attendedLessons)
            {
                attendedLessonInHours = lesson.StartTime.Hour - lesson.EndTime.Hour;
            }

            foreach (var course in activeCourses)
            {
                result.HourlySalaries.Add(new CalculatedTeacherSalaryModel.HourlySalary
                {
                    CourseId = course.Id,
                    CourseName = course.Name,
                    ActiveStudentsCount = course.CurrentStudentCount,
                    Hours = attendedLessonInHours,
                    Amount = Convert.ToDecimal(teacher.SalaryAmountPerHour),
                    Total = Convert.ToDecimal((decimal)attendedLessonInHours * teacher.SalaryAmountPerHour)
                });

                result.TotalSalary += Convert.ToDecimal((decimal)attendedLessonInHours * teacher.SalaryAmountPerHour);
            }

        }
        else if (teacher.PaymentType == TeacherSalaryType.Mixed)
        {
            foreach (var course in activeCourses)
            {
                result.MixedSalaries.Add(new CalculatedTeacherSalaryModel.MixedSalary
                {
                    CourseId = course.Id,
                    CourseName = course.Name,
                    ActiveStudentsCount = course.CurrentStudentCount,
                    FixSalary = Convert.ToDecimal(teacher.FixSalary),
                    Percent = Convert.ToInt32(teacher.SalaryPercentPerStudent),
                    Total = Convert.ToDecimal(teacher.FixSalary) + Convert.ToDecimal(((course.CurrentStudentCount * course.Price) / 100) * teacher.SalaryPercentPerStudent)
                });

                result.TotalSalary += Convert.ToDecimal(teacher.FixSalary) + Convert.ToDecimal(((course.CurrentStudentCount * course.Price) / 100) * teacher.SalaryPercentPerStudent);
            }
        }

        return result;
    }

    public List<TeacherPaymentGetModel> GetTeacherPaymentTypes()
    {
        var enumValues = enumService.GetEnumValues<TeacherSalaryType>();
        var paymentTypes = enumValues.Select(ev => new TeacherPaymentGetModel
        {
            Id = ev.Id,
            Name = ev.Name
        }).ToList();

        return paymentTypes;
    }

    public async Task<TeachersStatistics> GetStatisticsAsync(int companyId)
    {
        var query = unitOfWork.Teachers
            .SelectAllAsQueryable(t =>
                t.CompanyId == companyId);

        var statistics = await query
            .GroupBy(t => 1)
            .Select(g => new TeachersStatistics
            {
                TotalTeachersCount = g.Count(),
                TotalLeadTeachersCount = g.Count(t => t.Type == TeacherType.Lead),
                TotalAssistantTeachersCount = g.Count(t => t.Type == TeacherType.Assistant),
                TotalActiveTeacherCount = g.Count(t => t.Status == TeacherStatus.Active),
                TotalLeftTeacherCount = g.Count(t => t.Status == TeacherStatus.Left),
                TotalOnLeaveTeacherCount = g.Count(t => t.Status == TeacherStatus.OnLeave)
            })
            .FirstOrDefaultAsync();

        return statistics ?? new TeachersStatistics();
    }

    public Task<TeacherStatistics> GetTeacherStatisticsAsync(int teacherId)
    {
        throw new NotImplementedException();
    }
}
