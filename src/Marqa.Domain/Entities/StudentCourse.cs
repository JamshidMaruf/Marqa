namespace Marqa.Domain.Entities;

public class StudentCourse : BaseEntity
{
    public int StudentId { get; set; }
    public int CourseId { get; set; }

    // Navigation
    public Student Student { get; set; }
    public Course Course { get; set; }
}
