namespace Marqa.Service.Services.Lessons.Models;

public class LessonAttendanceModel
{
    public List<LessonAttendanceItem> Items { get; set; } = new();
    public class LessonAttendanceItem
    {
        public int StudentId { get; set; }
        public bool IsAttended { get; set; }
    }
}