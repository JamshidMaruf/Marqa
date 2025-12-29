namespace Marqa.Service.Services.Lessons.LessonSchedules.Models;

public class WeekLessonScheduleModel
{
    public int MondayCount { get; set; }
    public int TuesdayCount { get; set; }
    public int WednesdayCount { get; set; }
    public int ThursdayCount { get; set; }
    public int FridayCount { get; set; }
    public int SaturdayCount { get; set; }
    public int SundayCount { get; set; }
    
    public List<DailyLessonScheduleModel> DailyLessonSchedules { get; set; }
}