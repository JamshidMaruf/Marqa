using Marqa.DataAccess.Repositories;
using Marqa.Domain.Entities;
using Marqa.Domain.Enums;
using Marqa.Service.Exceptions;
using Marqa.Service.Services.Files;
using Marqa.Service.Services.Lessons.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Marqa.Service.Services.Lessons;

public class LessonService(
    IRepository<Lesson> lessonRepository,
    IRepository<LessonAttendance> lessonAttendanceRepository,
    IRepository<Student> studentRepository,
    IRepository<Employee> teacherRepository,  
    IRepository<LessonVideo> lessonVideoRepository,
    IFileService fileService)
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

    public async Task VideoUploadAsync(int id, IFormFile video)
    {
        _ = await lessonRepository.SelectAsync(id)
            ?? throw new NotFoundException($"Lesson was not found with this ID = {id}");

        var allowedExtensions = new string[]{".mp4", ".avi", ".mkv", ".mov"};
        
        if(!allowedExtensions.Contains(Path.GetExtension(video.FileName)))
            throw new ArgumentIsNotValidException("Not supported video format");
        
        var result = await fileService.UploadAsync(video, "Files/Videos");

        await lessonVideoRepository.InsertAsync(new LessonVideo
        {
            VideoName = result.FileName,
            VideoPath = result.FilePath,
            LessonId = id
        });
    }

    public async Task CheckUpAsync(LessonAttendanceModel model)
    {
        _ = await lessonRepository.SelectAsync(model.LessonId)
            ?? throw new NotFoundException($"Lesson was not found with ID = {model.LessonId}");

        _ = await studentRepository.SelectAsync(model.StudentId)
            ?? throw new NotFoundException($"Student was not found with ID = {model.StudentId}");

        var lessonAttendance = await lessonAttendanceRepository.SelectAllAsQueryable()
            .Where(la => la.LessonId == model.LessonId && la.StudentId == model.StudentId)
            .FirstOrDefaultAsync();

        if(!lesson.IsCompleted)
            lesson.IsCompleted = true;

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
}