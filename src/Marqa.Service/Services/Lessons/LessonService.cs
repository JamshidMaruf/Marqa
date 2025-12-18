using Marqa.Domain.Entities;
using Marqa.Domain.Enums;
using Marqa.Service.Exceptions;
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

        lessonForUpdation.StartTime = model.StartTime;
        lessonForUpdation.EndTime = model.EndTime;
        lessonForUpdation.Date = model.Date;

        foreach(var lessonteacher in lessonForUpdation.Teachers)
            unitOfWork.LessonTeachers.MarkAsDeleted(lessonteacher);

        var lessonTeachers = new List<LessonTeacher>();
        foreach (var teacherId in model.TeacherIds)
            lessonTeachers.Add(new LessonTeacher
            {
                TeacherId = teacherId,
                LessonId = lessonForUpdation.Id
            });

        await unitOfWork.LessonTeachers.InsertRangeAsync(lessonTeachers);

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
        var existLesson = await unitOfWork.Lessons.CheckExistAsync(l => l.Id == id);

        if (!existLesson)
            throw new NotFoundException($"Lesson was not found with this ID = {id}");

        if (fileService.IsVideoExtension(Path.GetExtension(video.FileName)))
            throw new ArgumentIsNotValidException("Not supported video format");

        var result = await fileService.UploadAsync(video, "Files/Videos");

        unitOfWork.LessonVideos.Insert(new LessonVideo
        {
            Asset = new Asset
            {
                FileName = result.FileName,
                FilePath = result.FilePath,
            },
            LessonId = id
        });

        await unitOfWork.SaveAsync();
    }

    public async Task CheckUpAsync(LessonAttendanceModel model)
    {
        var lesson = await unitOfWork.Lessons.SelectAsync(l => l.Id == model.LessonId)
            ?? throw new NotFoundException($"Lesson was not found with ID = {model.LessonId}");

        var existStudent = await unitOfWork.Students.CheckExistAsync(s => s.Id == model.StudentId);

        if (!existStudent)
            throw new NotFoundException($"Student was not found with ID = {model.StudentId}");

        var lessonAttendance = await unitOfWork.LessonAttendances
            .SelectAsync(la => la.LessonId == model.LessonId && la.StudentId == model.StudentId);

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