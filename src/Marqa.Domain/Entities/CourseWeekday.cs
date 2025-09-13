namespace Marqa.Domain.Entities;

public class CourseWeekday : BaseEntity
{
    public DayOfWeek Weekday { get; set; }
    public int CourseId { get; set; }

    // Navigation
    public Course Course { get; set; }
}
