using Marqa.Domain.Enums;

namespace Marqa.Service.Services.Courses.Models;

public class CourseUpdateViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Subject { get; set; }
    public IEnumerable<TeacherInfo> Teachers { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public decimal Price { get; set; }
    public CourseStatus Status  { get; set; }
    public int MaxStudentCount { get; set; }
    public string Description { get; set; }
    public IEnumerable<WeekInfo> Weekdays { get; set; }
    public IEnumerable<LessonInfo> Lessons { get; set; }
}