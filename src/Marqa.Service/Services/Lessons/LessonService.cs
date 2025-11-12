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

        unitOfWork.Lessons.Update(lessonForUpdation);

        await unitOfWork.SaveAsync();
    }

    public async Task ModifyAsync(int id, string name, HomeTaskStatus homeTaskStatus)
    {
        var lesson = await unitOfWork.Lessons.SelectAsync(l => l.Id == id)
            ?? throw new NotFoundException($"Lesson was not found with this ID = {id}");

        lesson.Name = name;
        lesson.HomeTaskStatus = homeTaskStatus;

        unitOfWork.Lessons.Update(lesson);

        await unitOfWork.SaveAsync();
    }

    public async Task VideoUploadAsync(int id, IFormFile video)
    {
        _ = await unitOfWork.Lessons.SelectAsync(l => l.Id == id)
            ?? throw new NotFoundException($"Lesson was not found with this ID = {id}");

        var allowedExtensions = new string[] { ".mp4", ".avi", ".mkv", ".mov", ".flv" };

        if (!allowedExtensions.Contains(Path.GetExtension(video.FileName)))
            throw new ArgumentIsNotValidException("Not supported video format");

        var result = await fileService.UploadAsync(video, "Files/Videos");

        unitOfWork.LessonVideos.Insert(new LessonVideo
        {
            FileName = result.FileName,
            FilePath = result.FilePath,
            LessonId = id
        });

        await unitOfWork.SaveAsync();
    }

    public async Task CheckUpAsync(LessonAttendanceModel model)
    {
        var lesson = await unitOfWork.Lessons.SelectAsync(l => l.Id == model.LessonId)
            ?? throw new NotFoundException($"Lesson was not found with ID = {model.LessonId}");

        _ = await unitOfWork.Students.SelectAsync(s => s.Id == model.StudentId)
            ?? throw new NotFoundException($"Student was not found with ID = {model.StudentId}");

        var lessonAttendance = await unitOfWork.LessonAttendances
            .SelectAllAsQueryable(la => la.LessonId == model.LessonId && la.StudentId == model.StudentId)
            .FirstOrDefaultAsync();

        if (lesson.Number == 1)
        {
            var course = await unitOfWork.Courses.SelectAsync(c => c.Id == lesson.CourseId);
            course.Status = CourseStatus.Active;
        }

        if (!lesson.IsCompleted)
            lesson.IsCompleted = true;

        if (lessonAttendance != null)
        {
            lessonAttendance.Status = model.Status;
            lessonAttendance.LateTimeInMinutes = model.LateTimeInMinutes;
        }
        else
        {
            unitOfWork.LessonAttendances.Insert(new LessonAttendance
            {
                LessonId = model.LessonId,
                StudentId = model.StudentId,
                Status = model.Status,
                LateTimeInMinutes = model.LateTimeInMinutes
            });

            await unitOfWork.SaveAsync();
        }
    }

    public Task<List<LessonViewModel>> GetByCourseIdAsync(int courseId)
    {
        return unitOfWork.Lessons.SelectAllAsQueryable(l => l.CourseId == courseId)
            .Select(l => new LessonViewModel
            {
                Id = l.Id,
                Name = l.Name,
                Date = l.Date,
                Number = l.Number,
                HomeTaskStatus = l.HomeTaskStatus
            })
            .ToListAsync();
    }
}