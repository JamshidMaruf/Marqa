using Marqa.DataAccess.Repositories;
using Marqa.Domain.Entities;
using Marqa.Domain.Enums;
using Marqa.Service.Exceptions;
using Marqa.Service.Services.Lessons.Models;
using Microsoft.EntityFrameworkCore;

namespace Marqa.Service.Services.Lessons;

public class LessonService(
    IRepository<Lesson> lessonRepository,
    IRepository<LessonAttendance> lessonAttendanceRepository,
    IRepository<Student> studentRepository,
    IRepository<Employee> teacherRepository,
    IRepository<Course> courseRepository)
    : ILessonService
{
    public async Task UpdateAsync(int id, LessonUpdateModel model)
    {
        var lessonForUpdation = await lessonRepository.SelectAsync(id)
            ?? throw new NotFoundException($"Lesson was not found with this ID = {id}");

        _ = await teacherRepository.SelectAsync(model.TeacherId)
            ?? throw new NotFoundException($"No teacher was found with ID = {model.TeacherId}");

        lessonForUpdation.StartTime = model.StartTime;
        lessonForUpdation.EndTime = model.EndTime;
        lessonForUpdation.Date = model.Date;
        lessonForUpdation.TeacherId = model.TeacherId;

        await lessonRepository.UpdateAsync(lessonForUpdation);
    }

    public async Task ModifyAsync(int id, string name, HomeTaskStatus homeTaskStatus)
    {
        var lesson = await lessonRepository.SelectAsync(id)
            ?? throw new NotFoundException($"Lesson was not found with this ID = {id}");

        lesson.Name = name;
        lesson.HomeTaskStatus = homeTaskStatus;

        await lessonRepository.UpdateAsync(lesson);
    }

    public async Task CheckUpAsync(LessonAttendanceModel model)
    {
        var lesson = await lessonRepository.SelectAsync(model.LessonId)
            ?? throw new NotFoundException($"Lesson was not found with ID = {model.LessonId}");

        _ = await studentRepository.SelectAsync(model.StudentId)
            ?? throw new NotFoundException($"Student was not found with ID = {model.StudentId}");

        var lessonAttendance = await lessonAttendanceRepository.SelectAllAsQueryable()
            .Where(la => la.LessonId == model.LessonId && la.StudentId == model.StudentId)
            .FirstOrDefaultAsync();

        if (lessonAttendance != null)
        {
            lessonAttendance.Status = model.Status;
            lessonAttendance.LateTimeInMinutes = model.LateTimeInMinutes;
        }
        else
        {
            await lessonAttendanceRepository.InsertAsync(new LessonAttendance
            {
                LessonId = model.LessonId,
                StudentId = model.StudentId,
                Status = model.Status,
                LateTimeInMinutes = model.LateTimeInMinutes
            });
        }
    }

    public Task UploadLessonVideoAsync(int lessonId, object video)
    {
        throw new NotImplementedException();
    }

    public async Task<List<LessonViewModel>> GetAllByCourseIdAsync(int courseId)
    {
        _ = await courseRepository.SelectAsync(courseId)
            ?? throw new NotFoundException($"No course was found with ID = {courseId}");

        return await lessonRepository
            .SelectAllAsQueryable()
            .Where(lesson => lesson.CourseId == courseId) 
            .Select(lesson => new LessonViewModel
            {
                Date = lesson.Date,
                Id = lesson.Id,
                Name = lesson.Name,
                Number = lesson.Number,
                HomeTaskStatus = lesson.HomeTaskStatus
            })
            .ToListAsync();
    }
}