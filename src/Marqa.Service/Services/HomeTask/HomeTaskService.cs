using Marqa.DataAccess.Repositories;
using Marqa.Domain.Entities;
using Marqa.Service.Exceptions;
using Marqa.Service.Services.HomeTask;
using Marqa.Service.Services.HomeTask.Models;
using Microsoft.EntityFrameworkCore;

namespace Marqa;
public class HomeTaskService : IHomeTaskService
{
    private readonly IRepository<Lesson> lessonRepo;
    private readonly IRepository<HomeTask> homeTaskRepo;
    public HomeTaskService()
    {
        lessonRepo = new Repository<Lesson>();
        homeTaskRepo = new Repository<HomeTask>();
    }
    public async Task CreateHomeTaskAsync(HomeTaskCreateModel model)
    {
        var existlesson = await lessonRepo.SelectAsync(model.LessonId)
            ?? throw new NotFoundException("Lesson is not found");

        await homeTaskRepo.InsertAsync(new HomeTask
        {
            LessonId = model.LessonId,
            Deadline = model.Deadline,
            Description = model.Description,
        });
    }

    public async Task UpdateHomeTaskAsync(int id, HomeTaskUpdateModel model)
    {
        var existHomeTask = await homeTaskRepo.SelectAsync(id)
            ?? throw new NotFoundException("Home task is not found");

        existHomeTask.Deadline = model.Deadline;
        existHomeTask.Description = model.Description;

        await homeTaskRepo.UpdateAsync(existHomeTask);
    }

    public async Task DeleteHomeTaskAsync(int id)
    {
        var existHomeTask = await homeTaskRepo.SelectAsync(id)
            ?? throw new NotFoundException("Home task is not found");

        await homeTaskRepo.DeleteAsync(existHomeTask);
    }

    public async Task<List<HomeTaskViewModel>> GetHomeTaskAsync(int lessonId)
    {
        var existHomeTask = homeTaskRepo.SelectAllAsQueryable()
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
