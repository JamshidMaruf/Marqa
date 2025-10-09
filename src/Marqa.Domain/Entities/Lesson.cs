namespace Marqa.Domain.Entities;

public class Lesson : Auditable
{
    public string Name { get; set; }
    public int Number { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public DateOnly Date { get; set; }
    public string Room { get; set; }
    public int CourseId { get; set; }
    public int TeacherId { get; set; }

    // Navigation
    public Course Course { get; set; }
    public Employee Teacher { get; set; }
    public ICollection<LessonVideo> Videos { get; set; }
    public ICollection<LessonFile> Files { get; set; }
    public ICollection<LessonAttendance> Attendances { get; set; }
}
