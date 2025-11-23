using FluentValidation;
using Marqa.DataAccess.UnitOfWork;
using Marqa.Domain.Entities;
using Marqa.Domain.Enums;
using Marqa.Service.Exceptions;
using Marqa.Service.Extensions;
using Marqa.Service.Services.Courses.Models;
using Marqa.Service.Validators.Courses;
using Microsoft.EntityFrameworkCore;

namespace Marqa.Service.Services.Courses;

public class CourseService(IUnitOfWork unitOfWork, 
    IValidator<CourseCreateModel> courseCreateValidator,
    IValidator<CourseUpdateModel> courseUpdateValidator) : ICourseService
{
    public async Task CreateAsync(CourseCreateModel model)
    {
        await courseCreateValidator.EnsureValidatedAsync(model);

        _ = await unitOfWork.Companies.SelectAsync(c => c.Id == model.CompanyId)
           ?? throw new NotFoundException("Company not found");

        _ = await unitOfWork.Subjects.SelectAsync(c => c.Id == model.SubjectId)
            ?? throw new NotFoundException("Subject not found");

        _ = await unitOfWork.Employees.SelectAsync(t => t.Id == model.TeacherId)
            ?? throw new NotFoundException("Teacher not found");

        var transaction = await unitOfWork.BeginTransactionAsync();
        try
        {

            Course createdCourse = new Course
            {
                Name = model.Name,
                SubjectId = model.SubjectId,
                EndTime = model.EndTime,
                LessonCount = model.LessonCount,
                StartDate = model.StartDate,
                StartTime = model.StartTime,
                TeacherId = model.TeacherId,
                Status = model.Status,
                Description = model.Description,
                MaxStudentCount = model.MaxStudentCount,
                CompanyId = model.CompanyId
            };

            unitOfWork.Courses.Insert(createdCourse);
            await unitOfWork.SaveAsync();


            var weekDays = new List<DayOfWeek>();
            var courseWeekDays = new List<CourseWeekday>();

            foreach (var weekDay in model.Weekdays)
            {
                courseWeekDays.Add(new CourseWeekday
                {
                    Weekday = weekDay,
                    CourseId = createdCourse.Id
                });

                weekDays.Add(weekDay);
            }

            await unitOfWork.CourseWeekdays.InsertRangeAsync(courseWeekDays);
            await unitOfWork.SaveAsync();


            await GenerateLessonAsync(
                createdCourse.Id,
                model.StartDate,
                model.StartTime,
                model.EndTime,
                model.Room,
                model.LessonCount,
                model.TeacherId,
                weekDays);
            await unitOfWork.SaveAsync();

            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task UpdateAsync(int id, CourseUpdateModel model)
    {
        await courseUpdateValidator.EnsureValidatedAsync(model);

        var existCourse = await unitOfWork.Courses
            .SelectAllAsQueryable(c => !c.IsDeleted, 
            includes: [ "Lessons", "CourseWeekdays"])
            .FirstOrDefaultAsync(t => t.Id == id)
            ?? throw new NotFoundException($"Course is not found with this ID {id}");

        existCourse.EndTime = model.EndTime;
        existCourse.TeacherId = model.TeacherId;
        existCourse.StartTime = model.StartTime;
        existCourse.StartDate = model.StartDate;
        existCourse.Status = model.Status;
        existCourse.MaxStudentCount = model.MaxStudentCount;
        existCourse.Description = model.Description;

        var transaction = await unitOfWork.BeginTransactionAsync();
        try
        {

            foreach (var lesson in existCourse.Lessons)
                unitOfWork.Lessons.MarkAsDeleted(lesson);
            
            await unitOfWork.SaveAsync();
            

            foreach (var weekday in existCourse.CourseWeekdays)
                unitOfWork.CourseWeekdays.MarkAsDeleted(weekday);

            await unitOfWork.SaveAsync();


            foreach (var weekDay in model.Weekdays)
                unitOfWork.CourseWeekdays.Insert(new CourseWeekday
                {
                    CourseId = id,
                    Weekday = weekDay,
                });

            await unitOfWork.SaveAsync();


            await GenerateLessonAsync(
               existCourse.Id,
               model.StartDate,
               model.StartTime,
               model.EndTime,
               model.Room,
               model.LessonCount,
               model.TeacherId,
               model.Weekdays);
            await unitOfWork.SaveAsync();

            unitOfWork.Courses.Update(existCourse);            
            await unitOfWork.SaveAsync();

            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
        }
    }

    public async Task DeleteAsync(int id)
    {
        var existCourse = await unitOfWork.Courses
            .SelectAllAsQueryable(
            predicate: c => !c.IsDeleted,
            includes: new[] { "Lessons", "CourseWeekdays" })
            .FirstOrDefaultAsync(t => t.Id == id)
            ?? throw new NotFoundException($"Course is not found with this ID {id}");
        
        foreach (var lesson in existCourse.Lessons)
             unitOfWork.Lessons.MarkAsDeleted(lesson);

        foreach (var weekday in existCourse.CourseWeekdays)
             unitOfWork.CourseWeekdays.MarkAsDeleted(weekday);

         unitOfWork.Courses.MarkAsDeleted(existCourse);

        await unitOfWork.SaveAsync();
    }

    public async Task<CourseViewModel> GetAsync(int id)
    {
        return await unitOfWork.Courses
            .SelectAllAsQueryable(
            predicate: c => !c.IsDeleted,
            includes: new[] {"Subject", "Teacher", "Lessons", "CourseWeekdays", "StudentCourses"})
            .Select(c => new CourseViewModel
            {
                Id = c.Id,
                Name = c.Name,
                EndTime = c.EndTime,
                LessonCount = c.Lessons.Count,
                StartDate = c.StartDate,
                StartTime = c.StartTime,
                Status = c.Status,
                AvailableStudentCount = c.StudentCourses.Count,
                Subject = new CourseViewModel.SubjectInfo
                {
                    SubjectId = c.SubjectId,
                    SubjectName = c.Subject.Name,
                },
                Teacher = new CourseViewModel.TeacherInfo
                {
                    Id = c.TeacherId,
                    FirstName = c.Teacher.FirstName,
                    LastName = c.Teacher.LastName,
                },
                Weekdays = c.CourseWeekdays.Select(cw => cw.Weekday).ToList(),
                Lessons = c.Lessons.Select(cl => new CourseViewModel.LessonInfo
                {
                    Id = cl.Id,
                    Date = cl.Date,
                    EndTime = cl.EndTime,
                    StartTime = cl.StartTime,
                    Room = cl.Room,
                }).OrderBy(l => l.Date).ToList(),
            })
            .FirstOrDefaultAsync(t => t.Id == id)
            ?? throw new NotFoundException($"Course is not found with this ID {id}");
    }

    public async Task<List<CourseViewModel>> GetAllAsync(int companyId, string search, int? subjectId = null)
    {
        var query = unitOfWork.Courses.SelectAllAsQueryable(
            predicate: c => c.CompanyId == companyId && !c.IsDeleted,
            includes: new [] { "Subject", "Teacher", "Lessons", "StudentCourses", "CourseWeekdays" });

        if (!string.IsNullOrEmpty(search))
            query = query.Where(t => t.Name.Contains(search));

        if (subjectId != null)
            query = query.Where(t => t.SubjectId == subjectId);

        return await query
            .Select(c => new CourseViewModel
            {
                Id = c.Id,
                Name = c.Name,
                EndTime = c.EndTime,
                LessonCount = c.Lessons.Count,
                StartDate = c.StartDate,
                StartTime = c.StartTime,
                Status = c.Status,
                AvailableStudentCount = c.StudentCourses.Count,
                Subject = new CourseViewModel.SubjectInfo
                {
                    SubjectId = c.SubjectId,
                    SubjectName = c.Subject.Name,
                },
                Teacher = new CourseViewModel.TeacherInfo
                {
                    Id = c.TeacherId,
                    FirstName = c.Teacher.FirstName,
                    LastName = c.Teacher.LastName,
                },
                Weekdays = c.CourseWeekdays.Select(cw => cw.Weekday).ToList(),
                Lessons = c.Lessons.Select(cl => new CourseViewModel.LessonInfo
                {
                    Id = cl.Id,
                    Date = cl.Date,
                    EndTime = cl.EndTime,
                    StartTime = cl.StartTime,
                    Room = cl.Room,
                }).OrderBy(l => l.Date).ToList(),
            })
            .ToListAsync();
    }

    public async Task AttachStudentAsync(int courseId, int studentId,StudentStatus status)
    {
        _ = await unitOfWork.Courses.SelectAsync(c => c.Id == courseId)
            ?? throw new NotFoundException("Course is not found");

        _ = await unitOfWork.Students.SelectAsync(s => s.Id == studentId)
            ?? throw new NotFoundException("Student is not found");

        unitOfWork.StudentCourses.Insert(new StudentCourse() { CourseId = courseId, StudentId = studentId, Status = status });
        
        await unitOfWork.SaveAsync();
    }

    public async Task DetachStudentAsync(int courseId, int studentId)
    {
        _ = await unitOfWork.Courses.SelectAsync(c => c.Id == courseId)
            ?? throw new NotFoundException("Course is not found");

        _ = await unitOfWork.Students.SelectAsync(s => s.Id == studentId)
            ?? throw new NotFoundException("Student is not found");

        var studentCourse = await unitOfWork.StudentCourses
            .SelectAllAsQueryable(predicate: s => s.StudentId == studentId && s.CourseId == courseId)
            .FirstOrDefaultAsync();
        
        studentCourse.Status = StudentStatus.Detached;

        if (studentCourse is not null)
            unitOfWork.StudentCourses.MarkAsDeleted(studentCourse);

        await unitOfWork.SaveAsync();
    }

    public async Task<List<MainPageCourseViewModel>> GetCoursesByStudentIdAsync(int studentId)
    {
        return await unitOfWork.StudentCourses
            .SelectAllAsQueryable(predicate: c => c.StudentId == studentId)
            .Include(c => c.Course)
            .ThenInclude(c => c.Lessons)
            .Select(c => new MainPageCourseViewModel
            {
                CourseId = c.CourseId,
                LessonNumber = (c.Course.Lessons.Where(l => l.IsCompleted).OrderByDescending(l => l.Date).First()).Number,
                CourseName = c.Course.Name,
                HomeTaskStatus = (c.Course.Lessons.Where(l => l.IsCompleted).OrderByDescending(l => l.Date).First()).HomeTaskStatus,
            })
            .ToListAsync();
    }

    private async Task GenerateLessonAsync(
        int courseId,
        DateOnly startDate,
        TimeOnly startTime,
        TimeOnly endTime,
        string room,
        int lessonCount,
        int teacherId,
        List<DayOfWeek> weekDays)
    {
        List<Lesson> lessons = new List<Lesson>
        {
            new Lesson
            {
                CourseId = courseId,
                StartTime = startTime,
                EndTime = endTime,
                Room = room,
                Number = 1,
                Date = startDate,
                TeacherId = teacherId
            }
        };

        var count = 2;
        var currentDate = startDate;

        await Task.Run(() =>
        {
            while (count <= lessonCount)
            {
                currentDate = currentDate.AddDays(1);

                if (weekDays.Contains(currentDate.DayOfWeek))
                {
                    lessons.Add(new Lesson
                    {
                        CourseId = courseId,
                        StartTime = startTime,
                        EndTime = endTime,
                        Room = room,
                        Number = count,
                        Date = currentDate,
                        TeacherId = teacherId
                    });

                    count++;
                }
            }
        });

        await unitOfWork.Lessons.InsertRangeAsync(lessons);
    }

    public Task<List<CoursePageCourseViewModel>> GetNameByStudentIdAsync(int studentId)
    {
        return unitOfWork.StudentCourses.SelectAllAsQueryable(
            predicate: sc => sc.StudentId == studentId,
            includes: new[] {"Course"})
            .Select(sc => new CoursePageCourseViewModel
            {
                Id = sc.CourseId,
                Name = sc.Course.Name
            })
            .ToListAsync();
    }
}