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
    IRepository<Employee> teacherRepository)
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

    public async Task ModifyAsync(int id,string name)
    {
        var lesson = await lessonRepository.SelectAsync(id)
            ?? throw new NotFoundException($"Lesson was not found with this ID = {id}");

        lesson.Name = name;

        await lessonRepository.UpdateAsync(lesson);
    }

    public async Task CheckUpAsync(LessonAttendanceModel model)
    {
        var lesson = await lessonRepository.SelectAsync(model.LessonId)
            ?? throw new NotFoundException($"Lesson was not found with ID = {model.LessonId}");

        _ = await studentRepository.SelectAsync(model.StudentId)
            ?? throw new NotFoundException($"Student was not found with ID = {model.StudentId}");

        int lateMinutes = 0;
        var lessonAttendance = await lessonAttendanceRepository.SelectAllAsQueryable()
            .Where(la => la.LessonId == model.LessonId && la.StudentId == model.StudentId)
            .FirstOrDefaultAsync();

        if (model.Status == AttendanceStatus.Late)
        {
            lateMinutes = (int)(DateTime.Now.TimeOfDay - TimeSpan.Parse(lesson.StartTime.ToString())).TotalMinutes;
        }

        if (lessonAttendance != null)
        {
            lessonAttendance.Status = model.Status;
            lessonAttendance.LateTimeInMinutes = lateMinutes;
        }
        else
        {
            await lessonAttendanceRepository.InsertAsync(new LessonAttendance
            {
                LessonId = model.LessonId,
                StudentId = model.StudentId,
                Status = model.Status,
                LateTimeInMinutes = lateMinutes
            });
        }
    }
}