namespace Marqa.Domain.Entities;

public class Lesson : Auditable
{
    public int CourseId { get; set; }
    public int Number { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public DateOnly Date { get; set; }
    public string Room { get; set; }

    // Navigation
    public Course Course { get; set; }
}
