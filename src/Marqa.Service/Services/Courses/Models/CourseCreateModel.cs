using Marqa.Domain.Entities;

namespace Marqa.Service.Services.Courses.Models;

public class CourseCreateModel
{
    public string Name { get; set; }
    public string Room { get; set; }
    public int SubjectId { get; set; } 
    public int TeacherId { get; set; }
    public int CompanyId { get; set; } 
    public int LessonCount { get; set; } 
    public DateOnly StartDate { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public List<DayOfWeek> Weekdays { get; set; } = new();
}