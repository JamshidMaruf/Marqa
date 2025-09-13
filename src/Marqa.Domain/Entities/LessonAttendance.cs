using Marqa.Domain.Enums;

namespace Marqa.Domain.Entities;

public class LessonAttendance : Auditable
{
    public int LessonId { get; set; }
    public int StudentId { get; set; }
    public AttendanceStatus Status { get; set; }

    // Navigation
    public Lesson Lesson { get; set; }
    public Student Student { get; set; }
}
