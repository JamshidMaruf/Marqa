using Marqa.Domain.Entities;
using Marqa.Domain.Enums;
using Marqa.Service.Exceptions;
using Marqa.Service.Services.HomeTasks.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Marqa.Service.Services.HomeTasks;

public class HomeTaskService(
    IUnitOfWork unitOfWork,
    IFileService fileService
    ) : IHomeTaskService
{
    public async Task CreateAsync(HomeTaskCreateModel model)
    {
        unitOfWork.HomeTasks.Insert(new HomeTask
        {
            LessonId = model.LessonId,
            Deadline = model.Deadline,
            Description = model.Description
        });

        await unitOfWork.SaveAsync();

        // TODO: Implement SMS notification for students
        // var students = await unitOfWork.Lessons.SelectAsync(l => l.Id == model.LessonId,
        //     includes: ["Course.Students", "Course.Students.Student.User"]);
        // foreach (var student in students.Course.Enrollments)
        // {
        //     BackgroundJob.Enqueue(() =>
        //     smsService.SendNotificationAsync(student.Student.User.Phone, "Homework has been uploaded!"));
        // }
    }
    
    public async Task UploadHomeTaskFileAsync(int homeTaskId, IFormFile file)
    {
        var existHomeTask = await unitOfWork.HomeTasks.CheckExistAsync(l => l.Id == homeTaskId);

        if (!existHomeTask)
            throw new NotFoundException($"Hometask was not found with this ID = {homeTaskId}");

        string[] imageExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".webp", ".svg", ".tiff", ".ico" };

        string[] fileExtensions = {".mp3", ".wav", ".ogg", ".m4a", ".flac",
            ".pdf", ".doc", ".docx", ".xls", ".xlsx", ".ppt", ".pptx", ".txt",
            ".zip", ".rar", ".7z", ".tar", ".gz",".cs", ".js", ".html", ".css", ".json", ".xml"};
        if (imageExtensions.Contains(Path.GetExtension(file.FileName)))
        {
            var result = await fileService.UploadAsync(file, "Files/Images");

            unitOfWork.HomeTaskFiles.Insert(new HomeTaskFile
            {
                Asset = new Asset
                {
                    FileName = result.FileName,
                    FilePath = result.FilePath,
                },
                HomeTaskId = homeTaskId
            });
        }
        else if (fileExtensions.Contains(Path.GetExtension(file.FileName)))
        {
            var result = await fileService.UploadAsync(file, "Files/File");

            unitOfWork.HomeTaskFiles.Insert(new HomeTaskFile
            {
                Asset = new Asset
                {
                    FileName = result.FileName,
                    FilePath = result.FilePath,
                },
                HomeTaskId = homeTaskId
            });
        }

        await unitOfWork.SaveAsync();
    }

    public async Task UpdateAsync(int id, HomeTaskUpdateModel model)
    {
        var existHomeTask = await unitOfWork.HomeTasks.SelectAsync(h => h.Id == id)
            ?? throw new NotFoundException("Home task is not found");

        existHomeTask.Deadline = model.Deadline;
        existHomeTask.Description = model.Description;

        unitOfWork.HomeTasks.Update(existHomeTask);

        await unitOfWork.SaveAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var existHomeTask = await unitOfWork.HomeTasks
            .SelectAsync(
            h => h.Id == id,
            includes: "HomeTaskFile")
            ?? throw new NotFoundException("Home task is not found");

        if (existHomeTask.HomeTaskFile != null)
        {
            unitOfWork.HomeTaskFiles.MarkAsDeleted(existHomeTask.HomeTaskFile);
        }

        unitOfWork.HomeTasks.MarkAsDeleted(existHomeTask);

        await unitOfWork.SaveAsync();
    }

    public async Task<List<HomeTaskViewModel>> GetAsync(int lessonId)
    {
        var existHomeTask = unitOfWork.HomeTasks
            .SelectAllAsQueryable(ht => ht.LessonId == lessonId && !ht.IsDeleted);

        if (existHomeTask == null)
            throw new NotFoundException("Home task is not found");

        return await existHomeTask.Select(homeTask => new HomeTaskViewModel
        {
            Id = homeTask.Id,
            LessonId = homeTask.LessonId,
            Deadline = homeTask.Deadline,
            Description = homeTask.Description,
        })
        .ToListAsync();
    }

    public async Task StudentHomeTaskCreateAsync(StudentHomeTaskCreateModel model)
    {
        var existHomeTask = await unitOfWork.HomeTasks.SelectAsync(h => h.Id == model.HomeTaskId)
            ?? throw new NotFoundException("Homa task not found!");

        var existStudent = await unitOfWork.Students.SelectAsync(h => h.Id == model.HomeTaskId)
            ?? throw new NotFoundException("Student not found!");

        if (existHomeTask.Deadline < DateTime.Now)
            throw new Exception("Homework time is over!");

        unitOfWork.StudentHomeTasks
           .Insert(new StudentHomeTask
           {
               HomeTaskId = model.HomeTaskId,
               StudentId = model.StudentId,
               UploadedAt = DateTime.Now,
               Status = StudentHomeTaskStatus.Pending,
           });

        await unitOfWork.SaveAsync();
    }

    public async Task UploadStudentHomeTaskFileAsync(int studentHomeTaskId, IFormFile file)
    {
        var existHometaskStudent = await unitOfWork.StudentHomeTasks.CheckExistAsync(h => h.Id == studentHomeTaskId);

        if (!existHometaskStudent)
            throw new NotFoundException($"Student hometask was not found with this ID = {studentHomeTaskId}!");

        string[] imageExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".webp", ".svg", ".tiff", ".ico" };

        string[] fileExtensions = {".mp3", ".wav", ".ogg", ".m4a", ".flac",
            ".pdf", ".doc", ".docx", ".xls", ".xlsx", ".ppt", ".pptx", ".txt",
            ".zip", ".rar", ".7z", ".tar", ".gz",".cs", ".js", ".html", ".css", ".json", ".xml"};
        if (imageExtensions.Contains(Path.GetExtension(file.FileName)))
        {
            var result = await fileService.UploadAsync(file, "Files/Images");

            unitOfWork.StudentHomeTaskFiles.Insert(new StudentHomeTaskFile
            {
                Asset = new Asset
                {
                    FileName = result.FileName,
                    FilePath = result.FilePath,
                },
                StudentHomeTaskId = studentHomeTaskId,

            });
        }
        else if (fileExtensions.Contains(Path.GetExtension(file.FileName)))
        {
            var result = await fileService.UploadAsync(file, "Files/File");

            unitOfWork.StudentHomeTaskFiles.Insert(new StudentHomeTaskFile
            {
                Asset = new Asset
                {
                    FileName = result.FileName,
                    FilePath = result.FilePath,
                },
                StudentHomeTaskId = studentHomeTaskId,
            });
        }

        await unitOfWork.SaveAsync();
    }

    public async Task HomeTaskAssessmentAsync(HomeTaskAssessmentModel model)
    {
        var existStudentHomeTask = await unitOfWork.StudentHomeTasks.SelectAsync(h => h.Id == model.StudentHomeTaskId)
            ?? throw new NotFoundException("No completed home task found!");

        existStudentHomeTask.Score = model.Score;
        existStudentHomeTask.Status = model.Status;
        existStudentHomeTask.Feedback = model.FeedBack;

        unitOfWork.StudentHomeTasks.Update(existStudentHomeTask);

        await unitOfWork.SaveAsync();
        // Send notification
    }
}
