namespace Marqa.Service.Services.Courses.Models;

public class LessonInfo
{
    public int Id { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public DateOnly Date { get; set; }
    public string Room { get; set; }
}