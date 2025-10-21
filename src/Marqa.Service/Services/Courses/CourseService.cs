using Marqa.DataAccess.UnitOfWork;
using Marqa.Domain.Entities;
using Marqa.Service.Exceptions;
using Marqa.Service.Services.Courses.Models;
using Microsoft.EntityFrameworkCore;

namespace Marqa.Service.Services.Courses;

public class CourseService(IUnitOfWork unitOfWork)
    : ICourseService
{
    // transaction bilan optimal usulda insert qilish kerak
    public async Task CreateAsync(CourseCreateModel model)
    {
        _ = await unitOfWork.Companies.SelectAsync(c => c.Id == model.CompanyId)
           ?? throw new NotFoundException("Company not found");

        _ = await unitOfWork.Subjects.SelectAsync(c => c.Id == model.SubjectId)
            ?? throw new NotFoundException("Subject not found");

        _ = await unitOfWork.Employees.SelectAsync(t => t.Id == model.TeacherId)
            ?? throw new NotFoundException("Teacher not found");

        var createdCourse = await unitOfWork.Courses.InsertAsync(new Course
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
        });

        var weekDays = new List<DayOfWeek>();
        foreach(var weekDay in model.Weekdays)
        {
            await unitOfWork.CourseWeekdays.InsertAsync(new CourseWeekday
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
            model.TeacherId,
            weekDays);
    }

    // transaction bilan optimal usulda insert qilish kerak
    public async Task UpdateAsync(int id, CourseUpdateModel model)
    {
        var existCourse = await unitOfWork.Courses
            .SelectAllAsQueryable()
            .Where(c => !c.IsDeleted)
            .Include(c => c.Lessons.Where(l => !l.IsDeleted))
            .Include(c => c.CourseWeekdays.Where(w => !w.IsDeleted))
            .FirstOrDefaultAsync(t => t.Id == id)
            ?? throw new NotFoundException($"Course is not found with this ID {id}");

        existCourse.EndTime = model.EndTime;
        existCourse.TeacherId = model.TeacherId;
        existCourse.StartTime = model.StartTime;
        existCourse.StartDate = model.StartDate;
        existCourse.Status = model.Status;
        existCourse.MaxStudentCount = model.MaxStudentCount;
        existCourse.Description = model.Description;   

        // Delete lessons
        foreach (var lesson in existCourse.Lessons)
            await unitOfWork.Lessons.DeleteAsync(lesson);

        // Delete course weekdays
        foreach(var weekday in existCourse.CourseWeekdays)
            await unitOfWork.CourseWeekdays.DeleteAsync(weekday);

        // Create course's weekdays
        foreach (var weekDay in model.Weekdays)
            await unitOfWork.CourseWeekdays.InsertAsync(new CourseWeekday
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
            model.TeacherId,
            model.Weekdays);

        await unitOfWork.Courses.UpdateAsync(existCourse);
    }

    public async Task DeleteAsync(int id)
    {
        var existCourse = await unitOfWork.Courses
            .SelectAllAsQueryable()
            .Where(c => !c.IsDeleted)
            .Include(c => c.Lessons)
            .Include(c => c.CourseWeekdays)
            .FirstOrDefaultAsync(t => t.Id == id)
            ?? throw new NotFoundException($"Course is not found with this ID {id}");

        // Delete lessons
        foreach (var lesson in existCourse.Lessons)
            await unitOfWork.Lessons.DeleteAsync(lesson);

        // Delete course's weekdays
        foreach (var weekday in existCourse.CourseWeekdays)
            await unitOfWork.CourseWeekdays.DeleteAsync(weekday);

        // Delete course
        await unitOfWork.Courses.DeleteAsync(existCourse);
    }

    public async Task<CourseViewModel> GetAsync(int id)
    {

        var existCourse = await unitOfWork.Courses
            .SelectAllAsQueryable()
            .Where(c => !c.IsDeleted)
            .Include(c => c.Subject)
            .Include(c => c.Teacher)
            .Include(c => c.Lessons.Where(l => !l.IsDeleted))
            .Include(c => c.CourseWeekdays.Where(cwd => !cwd.IsDeleted))
            .Include(c => c.Students)
            .Select(c => new CourseViewModel
            {
                Id = c.Id,
                Name = c.Name,
                EndTime = c.EndTime,
                LessonCount = c.Lessons.Count,
                StartDate = c.StartDate,
                StartTime = c.StartTime,
                Status = c.Status,
                AvailableStudentCount = c.Students.Count,
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

        return existCourse;
    }

    public async Task<List<CourseViewModel>> GetAllAsync(int companyId, string search, int? subjectId = null)
    {
        var query = unitOfWork.Courses
            .SelectAllAsQueryable()
            .Where(c => c.CompanyId == companyId && !c.IsDeleted)
            .Include(c => c.Subject)
            .Include(c => c.Teacher)
            .Include(c => c.Lessons)
            .Include(c => c.Students)
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
                Status = c.Status,
                AvailableStudentCount = c.Students.Count,
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

    public async Task AttachStudentAsync(int courseId, int studentId)
    {
        _ = await unitOfWork.Courses.SelectAsync(c => c.Id == courseId)
            ?? throw new NotFoundException("Course is not found");
        
        _ = await unitOfWork.Students.SelectAsync(s => s.Id == studentId)
            ?? throw new NotFoundException("Student is not found");

        await unitOfWork.StudentCourses.InsertAsync(new StudentCourse() { CourseId = courseId, StudentId = studentId });
    }

    public async Task DetachStudentAsync(int courseId, int studentId)
    {
        _ = await unitOfWork.Courses.SelectAsync(c => c.Id == courseId)
            ?? throw new NotFoundException("Course is not found");
        
        _ = await unitOfWork.Students.SelectAsync(s => s.Id == studentId)
            ?? throw new NotFoundException("Student is not found");

        var studentCourse = await unitOfWork.StudentCourses.SelectAllAsQueryable()
            .FirstOrDefaultAsync(s => s.StudentId == studentId && s.CourseId == courseId);

        if (studentCourse is not null)
            await unitOfWork.StudentCourses.DeleteAsync(studentCourse);
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
        // First Lesson
        await unitOfWork.Lessons.InsertAsync(new Lesson
        {
            CourseId = courseId,
            StartTime = startTime,
            EndTime = endTime,
            Room = room,
            Number = 1,
            Date = startDate,
            TeacherId = teacherId       
        });

        var count = 2;
        var currentDate = startDate;

        while (count <= lessonCount)
        {
            currentDate = currentDate.AddDays(1);

            if (weekDays.Contains(currentDate.DayOfWeek))
            {
                await unitOfWork.Lessons.InsertAsync(new Lesson
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
    }
}