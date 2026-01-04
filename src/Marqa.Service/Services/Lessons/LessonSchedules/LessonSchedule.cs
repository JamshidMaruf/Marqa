using Marqa.Service.Services.Lessons.LessonSchedules.Models;
using Microsoft.EntityFrameworkCore;

namespace Marqa.Service.Services.Lessons.LessonSchedules;

public class LessonSchedule(IUnitOfWork unitOfWork) : ILessonSchedule
{
    public async Task<WeekLessonScheduleModel> GetWeekLessonScheduleAsync(int companyId)
    {
        var dates = new List<DateOnly>();
        for (int i = 0; i < 7; i++)
        {
            dates.Add(DateOnly.FromDateTime(DateTime.Now.AddDays(i)));
        }

        var lessons = await unitOfWork.Lessons
            .SelectAllAsQueryable(l => l.Course.CompanyId == companyId && dates.Contains(l.Date))
            .Include(l => l.Course)
            .Include(l => l.Teachers)
            .ThenInclude(t => t.Teacher.User)
            .ToListAsync();

        var mondaycount = lessons.Count(l => l.Date.DayOfWeek == DayOfWeek.Monday);
        var tuesdaycount = lessons.Count(l => l.Date.DayOfWeek == DayOfWeek.Tuesday);
        var wednesdaycount = lessons.Count(l => l.Date.DayOfWeek == DayOfWeek.Wednesday);
        var thursdaycount = lessons.Count(l => l.Date.DayOfWeek == DayOfWeek.Thursday);
        var fridaycount = lessons.Count(l => l.Date.DayOfWeek == DayOfWeek.Friday);
        var saturdaycount = lessons.Count(l => l.Date.DayOfWeek == DayOfWeek.Saturday);
        var sundaycount = lessons.Count(l => l.Date.DayOfWeek == DayOfWeek.Sunday);

        var dailylessonSchedules = new List<DailyLessonScheduleModel>();
        foreach (var lesson in lessons)
        {
            dailylessonSchedules.Add(new DailyLessonScheduleModel
            {
                DayOfWeek = lesson.Date.DayOfWeek,
                CourseName = lesson.Course.Name,
                CourseLevel = lesson.Course.Level,
                TeacherNames = lesson.Teachers.Select(t => t.Teacher.User.FirstName + " " + t.Teacher.User.LastName).ToList(),
                Date = lesson.Date,
                StartTime = lesson.StartTime,
                EndTime = lesson.EndTime,
                StudentCount = lesson.Course.CurrentStudentCount
            });
        }

        return new WeekLessonScheduleModel
        {
            MondayCount = mondaycount,
            TuesdayCount = tuesdaycount,
            WednesdayCount = wednesdaycount,
            ThursdayCount = thursdaycount,
            FridayCount = fridaycount,
            SaturdayCount = saturdaycount,
            SundayCount = sundaycount,
            DailyLessonSchedules = dailylessonSchedules
        };
    }
}