namespace Marqa.Service.Services.Lessons.Models;

public class LessonUpdateModel
{
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public DateOnly Date { get; set; }
    public string Room { get; set; }
    public int TeacherId { get; set; }
}