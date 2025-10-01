using Marqa.DataAccess.Repositories;
using Marqa.Domain.Entities;
using Marqa.Domain.Enums;
using Marqa.Service.Exceptions;
using Marqa.Service.Services.Lessons.Models;

namespace Marqa.Service.Services.Lessons;

public class LessonService : ILessonService
{
    private readonly IRepository<Lesson> lessonRepository;
    private readonly IRepository<LessonAttendance> lessonAttendanceRepository;
    private readonly IRepository<Student> studentRepository;

    public LessonService()
    {
        lessonRepository = new Repository<Lesson>();
        lessonAttendanceRepository = new Repository<LessonAttendance>();
        studentRepository = new Repository<Student>();
    }

    public async Task UpdateAsync(int id, LessonUpdateModel model)
    {
        var lessonForUpdation = await lessonRepository.SelectAsync(id)
            ?? throw new NotFoundException($"Lesson is not found with this ID = {id}");

        lessonForUpdation.StartTime = model.StartTime;
        lessonForUpdation.EndTime = model.EndTime;
        lessonForUpdation.Date = model.Date;
        lessonForUpdation.TeacherId = model.TeacherId;

        await lessonRepository.UpdateAsync(lessonForUpdation);
    }

    public async Task CheckUpAsync(LessonAttendanceModel model)
    {
        var lesson = await lessonRepository.SelectAsync(model.LessonId)
            ?? throw new NotFoundException($"Lesson was not found with ID = {model.LessonId}");

        _ = await studentRepository.SelectAsync(model.StudentId)
            ?? throw new NotFoundException($"Student was not found with ID = {model.StudentId}");

        int lateMinutes = 0;

        if (model.Status == AttendanceStatus.Late)
        {
            lateMinutes = (int) (DateTime.Now.TimeOfDay - TimeSpan.Parse(lesson.StartTime.ToString())).TotalMinutes;
        }

        await lessonAttendanceRepository.InsertAsync(new LessonAttendance
        {
            LessonId = model.LessonId,
            StudentId = model.StudentId,
            Status = model.Status,
            LateTimeInMinutes = lateMinutes
        });
    }
}