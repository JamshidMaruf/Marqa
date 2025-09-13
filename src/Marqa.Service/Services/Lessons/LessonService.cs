using Marqa.DataAccess.Repositories;
using Marqa.Domain.Entities;
using Marqa.Service.Exceptions;
using Marqa.Service.Services.Lessons.Models;

namespace Marqa.Service.Services.Lessons;

public class LessonService : ILessonService
{
    private readonly IRepository<Lesson> lessoonRepository;
    public LessonService()
    {
        lessoonRepository = new Repository<Lesson>();
    }

    public async Task UpdateAsync(int id, LessonUpdateModel model)
    {
        var existLesson = await lessoonRepository.SelectAsync(id)
            ?? throw new NotFoundException($"Lesson is not found with this ID = {id}");

        existLesson.StartTime = model.StartTime;
        model.EndTime = model.EndTime;
        existLesson.Date = model.Date;

        await lessoonRepository.UpdateAsync(existLesson);
    }
}