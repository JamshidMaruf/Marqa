using Marqa.Domain.Enums;

namespace Marqa.Service.Services.Courses.Models;

public class CourseUpdateModel
{
    public string Name { get; set; }
    public string Room { get; set; }
    public int TeacherId { get; set; }
    public decimal Price { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }   
    public string Level { get; set; }
    public CourseStatus Status  { get; set; }
    public int MaxStudentCount { get; set; }
    public string Description { get; set; }
    public List<CourseCreateModel.Weekday> Weekdays { get; set; } = new();

}