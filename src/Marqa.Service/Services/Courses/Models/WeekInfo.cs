namespace Marqa.Service.Services.Courses.Models;

public class WeekInfo
{
    public int Id { get; set; }
    public string Name { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
}