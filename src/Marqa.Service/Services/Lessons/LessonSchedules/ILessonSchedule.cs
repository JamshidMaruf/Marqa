using Marqa.Service.Services.Lessons.LessonSchedules.Models;

namespace Marqa.Service.Services.Lessons;

public interface ILessonSchedule : IScopedService
{
    Task<WeekLessonScheduleModel> GetWeekLessonScheduleAsync(int companyId);
}