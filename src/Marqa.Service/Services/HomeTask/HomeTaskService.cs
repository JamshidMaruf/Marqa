using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Marqa.DataAccess.Repositories;
using Marqa.Domain.Entities;
using Marqa.Service.Exceptions;
using Marqa.Service.Services.HomeTask;
using Marqa.Service.Services.HomeTask.Models;

namespace Marqa;
public class HomeTaskService : IHomeTaskService
{
    private readonly IRepository<Lesson> _lessonRepo;
    private readonly IRepository<HomeTask> _homeTaskRepo;
    public HomeTaskService()
    {
        _lessonRepo = new Repository<Lesson>();
        _homeTaskRepo = new Repository<HomeTask>();
    }
    public async Task CreateHomeTaskAsync(HomeTaskCreateModel model)
    {
        var existlesson = await _lessonRepo.SelectAsync(model.LessonId)
            ?? throw new NotFoundException("Lesson is not found");

        await _homeTaskRepo.InsertAsync(new HomeTask
        {
            LessonId = model.LessonId,
            Deadline = model.Deadline,
            Description = model.Description,
            CreatedAt = DateTime.UtcNow,
            IsDeleted = false,
            Lesson = existlesson
        });
    }

    public async Task DeleteHomeTaskAsync(int lessonid)
    {
        var existHomeTask = _homeTaskRepo.SelectAllAsQueryable()
            .FirstOrDefault(ht => ht.LessonId == lessonid && !ht.IsDeleted)
            ?? throw new NotFoundException("Home task is not found");
        await _homeTaskRepo.DeleteAsync(existHomeTask);
    }

    public async Task<HomeTaskGetModel> GetHomeTaskAsync(int lessonid)
    {
        var existHomeTask = _homeTaskRepo.SelectAllAsQueryable()
            .FirstOrDefault(ht => ht.LessonId == lessonid && !ht.IsDeleted)
            ?? throw new NotFoundException("Home task is not found");

        return new HomeTaskGetModel
        {
            LessonId = existHomeTask.LessonId,
            Deadline = existHomeTask.Deadline,
            Description = existHomeTask.Description
        };
    }

    public async Task UpdateHomeTaskAsync(HomeTaskUpdateModel model)
    {
        var existHomeTask = _homeTaskRepo.SelectAllAsQueryable()
            .FirstOrDefault(ht => ht.LessonId == model.LessonId && !ht.IsDeleted)
            ?? throw new NotFoundException("Home task is not found");

        existHomeTask.Deadline = model.Deadline;
        existHomeTask.Description = model.Description;

        await _homeTaskRepo.UpdateAsync(existHomeTask);
    }
}
