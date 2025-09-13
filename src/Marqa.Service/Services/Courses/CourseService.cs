using Marqa.DataAccess.Repositories;
using Marqa.Domain.Entities;
using Marqa.Service.Exceptions;
using Marqa.Service.Services.Courses.Models;
using Marqa.Service.Services.Lessons.Models;
using Marqa.Service.Services.Teachers.Models;
using Microsoft.EntityFrameworkCore;

namespace Marqa.Service.Services.Courses;

public class CourseService : ICourseService
{
    private readonly IRepository<Lesson> lessonRepository;
    private readonly IRepository<Course> courseRepository;
    private readonly IRepository<Subject> subjectRepository;
    private readonly IRepository<Teacher> teacherRepository;
    private readonly IRepository<Company> companyRepository;
    private readonly IRepository<CourseWeekday> courseWeekdayRepository;
    public CourseService()
    {
        lessonRepository = new Repository<Lesson>();
        courseRepository = new Repository<Course>();
        subjectRepository = new Repository<Subject>();
        teacherRepository = new Repository<Teacher>();
        companyRepository = new Repository<Company>();
        courseWeekdayRepository = new Repository<CourseWeekday>();
    }

    public async Task CreateAsync(CourseCreateModel model)
    {
        _ = await companyRepository.SelectAsync(model.CompanyId)
           ?? throw new NotFoundException("Company not found");

        _ = await subjectRepository.SelectAsync(model.SubjectId)
            ?? throw new NotFoundException("Subject not found");

        _ = await teacherRepository.SelectAsync(model.TeacherId)
            ?? throw new NotFoundException("Teacher not found");

        var createdCourse = await courseRepository.InsertAsync(new Course
        {
            Name = model.Name,
            SubjectId = model.SubjectId,
            EndTime = model.EndTime,
            LessonCount = model.LessonCount,
            StartDate = model.StartDate,
            StartTime = model.StartTime,
            TeacherId = model.TeacherId
        });

        var weekDays = new List<DayOfWeek>();
        foreach(var weekDay in model.Weekdays)
        {
            await courseWeekdayRepository.InsertAsync(new CourseWeekday
            {
                Weekday = weekDay,
                CourseId = createdCourse.Id
            });

            weekDays.Add(weekDay);
        }

        await GenerateLessonAsync(
            createdCourse.Id,
            model.StartDate,
            model.StartTime,
            model.EndTime,
            model.Room,
            model.LessonCount,
            weekDays);
    }

    public async Task UpdateAsync(int id, CourseUpdateModel model)
    {
        var existCourse = await courseRepository
            .SelectAllAsQueryable()
            .Include(c => c.Lessons)
            .Include(c => c.CourseWeekdays)
            .FirstOrDefaultAsync(t => t.Id == id)
            ?? throw new NotFoundException($"Course is not found with this ID {id}");

        existCourse.EndTime = model.EndTime;
        existCourse.TeacherId = model.TeacherId;
        existCourse.StartTime = model.StartTime;
        existCourse.StartDate = model.StartDate;

        // Delete lessons
        foreach (var lesson in existCourse.Lessons)
            await lessonRepository.DeleteAsync(lesson);

        // Delete course weekdays
        foreach(var weekday in existCourse.CourseWeekdays)
            await courseWeekdayRepository.DeleteAsync(weekday);

        // Create course's weekdays
        foreach (var weekDay in model.Weekdays)
            await courseWeekdayRepository.InsertAsync(new CourseWeekday
            {
                CourseId = id,
                Weekday = weekDay,
            });

        // Generate lessons
        await GenerateLessonAsync(
            existCourse.Id,
            model.StartDate,
            model.StartTime,
            model.EndTime,
            model.Room,
            model.LessonCount,
            model.Weekdays);

        await courseRepository.UpdateAsync(existCourse);
    }

    public async Task DeleteAsync(int id)
    {
        var existCourse = await courseRepository
            .SelectAllAsQueryable()
            .Include(c => c.Lessons)
            .Include(c => c.CourseWeekdays)
            .FirstOrDefaultAsync(t => t.Id == id)
            ?? throw new NotFoundException($"Course is not found with this ID {id}");

        // Delete lessons
        foreach (var lesson in existCourse.Lessons)
            await lessonRepository.DeleteAsync(lesson);

        // Delete course's weekdays
        foreach (var weekday in existCourse.CourseWeekdays)
            await courseWeekdayRepository.DeleteAsync(weekday);

        // Delete course
        await courseRepository.DeleteAsync(existCourse);
    }

    public async Task<CourseViewModel> GetAsync(int id)
    {
        var existCourse = await courseRepository
            .SelectAllAsQueryable()
            .Include(c => c.Subject)
            .Include(c => c.Teacher)
            .Include(c => c.Lessons)
            .Include(c => c.CourseWeekdays)
            .Select(c => new CourseViewModel
            {
                Id = id,
                Name = c.Name,
                EndTime = c.EndTime,
                LessonCount = c.Lessons.Count,
                StartDate = c.StartDate,
                StartTime = c.StartTime,
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
                }).ToList(),
            })
            .FirstOrDefaultAsync(t => t.Id == id)
            ?? throw new NotFoundException($"Course is not found with this ID {id}");

        return existCourse;
    }

    public async Task<List<CourseViewModel>> GetAllAsync(int companyId, string search, int? subjectId = null)
    {
        var query = courseRepository
            .SelectAllAsQueryable()
            .Where(c => c.CompanyId == companyId)
            .Include(c => c.Subject)
            .Include(c => c.Teacher)
            .Include(c => c.Lessons)
            .Include(c => c.CourseWeekdays)
            .AsQueryable();

        if(!string.IsNullOrEmpty(search))
            query = query.Where(t => t.Name.Contains(search));

        if(subjectId != null)
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
                }).ToList(),
            })
            .ToListAsync();
    }

    private async Task GenerateLessonAsync(
        int courseId, 
        DateOnly startDate,
        TimeSpan startTime,
        TimeSpan endTime,
        string room,
        int lessonCount,
        List<DayOfWeek> weekDays)
    {
        // First Lesson
        await lessonRepository.InsertAsync(new Lesson
        {
            CourseId = courseId,
            StartTime = startTime,
            EndTime = endTime,
            Room = room,
            Number = 1,
            Date = startDate
        });

        var count = 2;
        var currentDate = startDate;

        while (count <= lessonCount)
        {
            currentDate = currentDate.AddDays(1);

            if (weekDays.Contains(currentDate.DayOfWeek))
            {
                await lessonRepository.InsertAsync(new Lesson
                {
                    CourseId = courseId,
                    StartTime = startTime,
                    EndTime = endTime,
                    Room = room,
                    Number = count,
                    Date = currentDate,
                });

                count++;
            }
        }
    }
}