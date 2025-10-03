using Marqa.DataAccess.Repositories;
using Marqa.Domain.Entities;
using Marqa.Service.Exceptions;
using Marqa.Service.Services.HomeTask;
using Marqa.Service.Services.HomeTask.Models;
using Microsoft.EntityFrameworkCore;

namespace Marqa;
public class HomeTaskService(IRepository<Lesson> lessonRepository, IRepository<HomeTask> homeTaskRepository)
    : IHomeTaskService
{
    public async Task CreateHomeTaskAsync(HomeTaskCreateModel model)
    {
        var existLesson = await lessonRepository.SelectAsync(model.LessonId)
            ?? throw new NotFoundException("Lesson is not found");

        await homeTaskRepository.InsertAsync(new HomeTask
        {
            LessonId = model.LessonId,
            Deadline = model.Deadline,
            Description = model.Description,
        });
    }

    public async Task UpdateHomeTaskAsync(int id, HomeTaskUpdateModel model)
    {
        var existHomeTask = await homeTaskRepository.SelectAsync(id)
            ?? throw new NotFoundException("Home task is not found");

        existHomeTask.Deadline = model.Deadline;
        existHomeTask.Description = model.Description;

        await homeTaskRepository.UpdateAsync(existHomeTask);
    }

    public async Task DeleteHomeTaskAsync(int id)
    {
        var existHomeTask = await homeTaskRepository.SelectAsync(id)
            ?? throw new NotFoundException("Home task is not found");

        await homeTaskRepository.DeleteAsync(existHomeTask);
    }

    public async Task<List<HomeTaskViewModel>> GetHomeTaskAsync(int lessonId)
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
}
