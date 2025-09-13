namespace Marqa.Domain.Entities;

public class Exam : BaseEntity
{
    public int CourseId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string Title { get; set; }

    // Navigation
    public Course Course { get; set; }
}
