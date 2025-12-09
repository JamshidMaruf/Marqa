using Marqa.Domain.Enums;

namespace Marqa.Service.Services.Courses.Models;

public class CourseCreateModel
{
    public string Name { get; set; }
    public string Room { get; set; }
    public int SubjectId { get; set; } 
    public int TeacherId { get; set; }
    public int CompanyId { get; set; } 
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public decimal Price { get; set; }
    public string Level { get; set; }
    public CourseStatus Status  { get; set; }
    public int MaxStudentCount { get; set; }
    public string Description { get; set; }
    public List<Weekday> Weekdays { get; set; } = new();
    
    public class  Weekday
    {
        public  DayOfWeek DayOfWeek { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
    }
}