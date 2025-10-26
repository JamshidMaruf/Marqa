using Marqa.DataAccess.Repositories;
using Marqa.DataAccess.UnitOfWork;
using Marqa.Domain.Entities;
using Marqa.Domain.Enums;
using Marqa.Service.Exceptions;
using Marqa.Service.Services.Files;
using Marqa.Service.Services.Lessons.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Marqa.Service.Services.Lessons;

public class LessonService(
    IUnitOfWork unitOfWork, 
    IFileService fileService) : ILessonService
{
    public async Task UpdateAsync(int id, LessonUpdateModel model)
    {
        var lessonForUpdation = await unitOfWork.Lessons.SelectAsync(l => l.Id == id)
            ?? throw new NotFoundException($"Lesson was not found with this ID = {id}");

        _ = await unitOfWork.Employees.SelectAsync(t => t.Id == model.TeacherId)
            ?? throw new NotFoundException($"No teacher was found with ID = {model.TeacherId}");

        lessonForUpdation.StartTime = model.StartTime;
        lessonForUpdation.EndTime = model.EndTime;
        lessonForUpdation.Date = model.Date;
        lessonForUpdation.TeacherId = model.TeacherId;

        await unitOfWork.Lessons.Update(lessonForUpdation);
    }

    public async Task ModifyAsync(int id, string name, HomeTaskStatus homeTaskStatus)
    {
        var lesson = await unitOfWork.Lessons.SelectAsync(l => l.Id == id)
            ?? throw new NotFoundException($"Lesson was not found with this ID = {id}");

        lesson.Name = name;
        lesson.HomeTaskStatus = homeTaskStatus;

        await unitOfWork.Lessons.Update(lesson);
    }

    public async Task VideoUploadAsync(int id, IFormFile video)
    {
        _ = await unitOfWork.Lessons.SelectAsync(l => l.Id == id)
            ?? throw new NotFoundException($"Lesson was not found with this ID = {id}");

        var allowedExtensions = new string[]{".mp4", ".avi", ".mkv", ".mov"};
        
        if(!allowedExtensions.Contains(Path.GetExtension(video.FileName)))
            throw new ArgumentIsNotValidException("Not supported video format");
        
        var result = await fileService.UploadAsync(video, "Files/Videos");

        await unitOfWork.LessonVideos.Insert(new LessonVideo
        {
            FileName = result.FileName,
            FilePath = result.FilePath,
            LessonId = id
        });
    }

    public async Task CheckUpAsync(LessonAttendanceModel model)
    {
        var lesson = await unitOfWork.Lessons.SelectAsync(l => l.Id == model.LessonId)
            ?? throw new NotFoundException($"Lesson was not found with ID = {model.LessonId}");

        _ = await unitOfWork.Students.SelectAsync(s => s.Id == model.StudentId)
            ?? throw new NotFoundException($"Student was not found with ID = {model.StudentId}");

        var lessonAttendance = await unitOfWork.LessonAttendances.SelectAllAsQueryable()
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
            await unitOfWork.LessonAttendances.Insert(new LessonAttendance
            {
                LessonId = model.LessonId,
                StudentId = model.StudentId,
                Status = model.Status,
                LateTimeInMinutes = model.LateTimeInMinutes
            });
        }
    }
}