using Marqa.Domain.Enums;

namespace Marqa.Service.Services.Courses.Models;

public class CourseUpdateModel
{
    public string Name { get; set; }
    public string Room { get; set; }
    public int TeacherId { get; set; }
    public int LessonCount { get; set; }
    public DateOnly StartDate { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public CourseStatus Status  { get; set; }
    public int MaxStudentCount { get; set; }
    public string Description { get; set; }
    public List<DayOfWeek> Weekdays { get; set; } = new();

}