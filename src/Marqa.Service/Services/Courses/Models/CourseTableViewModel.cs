using Marqa.Domain.Enums;

namespace Marqa.Service.Services.Courses.Models;

public class CourseTableViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Subject { get; set; }
    public string Level { get; set; }
    public CourseStatus Status { get; set; }
    public IEnumerable<TeacherInfo> Teachers { get; set; }
    public IEnumerable<WeekInfo> Weekdays { get; set; }
    public int LessonCount { get; set; }
    public decimal Price { get; set; }
    public int MaxStudentCount { get; set; }
    public int AvailableStudentCount { get; set; }
}