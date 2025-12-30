namespace Marqa.Service.Services.Lessons.LessonSchedules.Models;

public class DailyLessonScheduleModel
{
    public DayOfWeek DayOfWeek { get; set; }
    public string CourseName { get; set; }
    public string CourseLevel { get; set; }

    public List<string> TeacherNames { get; set; } = new();
    public DateOnly Date { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }

    public int StudentCount { get; set; }
}