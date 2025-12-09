namespace Marqa.Domain.Entities;

public class CourseWeekday : Auditable
{
    public DayOfWeek Weekday { get; set; }
    public TimeOnly StartTime { get; set; } 
    public TimeOnly EndTime { get; set; }
    public int CourseId { get; set; }

    // Navigation
    public Course Course { get; set; }
}

