namespace Marqa.Domain.Entities;

public class Course : BaseEntity
{
    public string Name { get; set; }
    public int SubjectId { get; set; }
    public int TeacherId { get; set; }
    public int LessonCount { get; set; }
    public DateOnly StartDate { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public int CompanyId { get; set; }

    // Navigation
    public Company Company { get; set; }
    public Subject Subject { get; set; }
    public Teacher Teacher { get; set; }
    public ICollection<CourseWeekday> CourseWeekdays { get; set; }
    public ICollection<Lesson> Lessons { get; set; }
    public ICollection<Exam> Exams { get; set; }
}
