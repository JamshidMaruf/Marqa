namespace Marqa.Service.Services.Lessons.Models;

public class LessonUpdateModel
{
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public DateOnly Date { get; set; }
    public string Room { get; set; }
    public IEnumerable<int> TeacherIds { get; set; }
}
