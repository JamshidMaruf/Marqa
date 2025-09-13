namespace Marqa.Domain.Entities;

public class LessonAttendance : BaseEntity
{
    public int LessonId { get; set; }
    public int StudentId { get; set; }
    public bool IsAttended { get; set; }

    // Navigation
    public Lesson Lesson { get; set; }
    public Student Student { get; set; }
}
