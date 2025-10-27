﻿using Marqa.DataAccess.UnitOfWork;
using Marqa.Domain.Entities;
using Marqa.Domain.Enums;
using Marqa.Service.Exceptions;
using Marqa.Service.Services.HomeTasks.Models;
using Microsoft.EntityFrameworkCore;

namespace Marqa.Service.Services.HomeTasks;

public class HomeTaskService(IUnitOfWork unitOfWork) : IHomeTaskService
{
    public async Task CreateAsync(HomeTaskCreateModel model)
    {
        var existLesson = await unitOfWork.Lessons.SelectAsync(l => l.Id == model.LessonId)
            ?? throw new NotFoundException("Lesson is not found");

        unitOfWork.HomeTasks.Insert(new HomeTask
        {
            LessonId = model.LessonId,
            Deadline = model.Deadline,
            Description = model.Description
        });

        await unitOfWork.SaveAsync();
        // Send Notification
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
        var existHomeTask = await unitOfWork.HomeTasks.SelectAsync(h => h.Id == id)
            ?? throw new NotFoundException("Home task is not found");

        unitOfWork.HomeTasks.Delete(existHomeTask);

        await unitOfWork.SaveAsync();
    }

    public async Task<List<HomeTaskViewModel>> GetAsync(int lessonId)
    {
        var existHomeTask = unitOfWork.HomeTasks.SelectAllAsQueryable()
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
                Info = model.Info,
                UploadedAt = DateTime.Now,
                Status = StudentHomeTaskStatus.accepted,
            });

        await unitOfWork.SaveAsync();
    }

    public async Task HomeTaskAssessmentAsync(HomeTaskAssessmentModel model)
    {
        var existHomeTaskUpload = await unitOfWork.StudentHomeTasks.SelectAsync(h => h.Id == model.StudentHomeTaskId)
            ?? throw new NotFoundException("No completed home task found!");

        unitOfWork.StudentHomeTaskFeedbacks
            .Insert(new StudentHomeTaskFeedback
            {
                StudentHomeTaskId = model.StudentHomeTaskId,
                TeacherId = model.TeacherId,
                FeedBack = model.FeedBack
            });

        await unitOfWork.SaveAsync();

        existHomeTaskUpload.Score = model.Score;
        
        await unitOfWork.SaveAsync();
        // Send notification
    }
}
