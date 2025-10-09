using Marqa.DataAccess.Repositories;
using Marqa.Domain.Entities;
using Marqa.Domain.Enums;
using Marqa.Service.Exceptions;
using Marqa.Service.Services.HomeTasks.Models;
using Microsoft.EntityFrameworkCore;

namespace Marqa.Service.Services.HomeTasks;

public class HomeTaskService(
    IRepository<Lesson> lessonRepository, 
    IRepository<HomeTask> homeTaskRepository,
    IRepository<Student> studentRepository,
    IRepository<StudentHomeTask> studentHomeTaskRepository,
    IRepository<StudentHomeTaskFeedback> studentHomeTaskFeedbackRepository
    ) : IHomeTaskService
{
    public async Task CreateAsync(HomeTaskCreateModel model)
    {
        var existLesson = await lessonRepository.SelectAsync(model.LessonId)
            ?? throw new NotFoundException("Lesson is not found");

        await homeTaskRepository.InsertAsync(new HomeTask
        {
            LessonId = model.LessonId,
            Deadline = model.Deadline,
            Description = model.Description,
        });
        
        // Send Notification
    }

    public async Task UpdateAsync(int id, HomeTaskUpdateModel model)
    {
        var existHomeTask = await homeTaskRepository.SelectAsync(id)
            ?? throw new NotFoundException("Home task is not found");

        existHomeTask.Deadline = model.Deadline;
        existHomeTask.Description = model.Description;

        await homeTaskRepository.UpdateAsync(existHomeTask);
    }

    public async Task DeleteAsync(int id)
    {
        var existHomeTask = await homeTaskRepository.SelectAsync(id)
            ?? throw new NotFoundException("Home task is not found");

        await homeTaskRepository.DeleteAsync(existHomeTask);
    }

    public async Task<List<HomeTaskViewModel>> GetAsync(int lessonId)
    {
        var existHomeTask = homeTaskRepository.SelectAllAsQueryable()
            .Where(ht => ht.LessonId == lessonId && !ht.IsDeleted);

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

    public async Task StudentHomeTaskUploadAsync(HomeTaskUploadCreateModel model)
    {
        var existHomeTask = await homeTaskRepository.SelectAsync(model.HomeTaskId)
            ?? throw new NotFoundException("Homa task not found!");

        var existStudent = await studentRepository.SelectAsync(model.StudentId)
            ?? throw new NotFoundException("Student not found!");

        if (existHomeTask.Deadline < DateTime.Now)
            throw new Exception("Homework time is over!");

        var createUpload = await studentHomeTaskRepository
            .InsertAsync(new StudentHomeTask
            {
                HomeTaskId = model.HomeTaskId,
                StudentId = model.StudentId,
                Info = model.Info,
                UploadedAt = DateTime.Now,
                Status = StudentHomeTaskStatus.accepted,
            });
    }

    public async Task HomeTaskAssessmentAsync(HomeTaskAssessmentModel model)
    {
        var existHomeTaskUpload = await studentHomeTaskRepository.SelectAsync(model.StudentHomeTaskId)
            ?? throw new NotFoundException("No completed home task found!");

        await studentHomeTaskFeedbackRepository
            .InsertAsync(new StudentHomeTaskFeedback
            {
                StudentHomeTaskId = model.StudentHomeTaskId,
                TeacherId = model.TeacherId,
                FeedBack = model.FeedBack
            });

        existHomeTaskUpload.Score = model.Score;
        
        // Send notification
    }
}
