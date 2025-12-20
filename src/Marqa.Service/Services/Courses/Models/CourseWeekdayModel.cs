namespace Marqa.Service.Services.Courses.Models;

public class CourseWeekdayModel
{
    public DayOfWeek DayOfWeek { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
}