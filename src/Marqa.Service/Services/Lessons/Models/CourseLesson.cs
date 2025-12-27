namespace Marqa.Service.Services.Lessons.Models;

public class CourseLesson
{
    public int CourseId { get; set; }
    public string CourseName { get; set; }
    public int TeacherId { get; set; }
    public string TeacherName { get; set; }
    public int CourseStudentsCount { get; set; }
    public int CoursePresentStudentsCount { get; set; }
    public decimal AttendPercentage { get; set; }
    public bool IsCheckedUp{ get; set; }
    public int LessonNumber { get; set; }
    public int LessonId{ get; set; }
}
