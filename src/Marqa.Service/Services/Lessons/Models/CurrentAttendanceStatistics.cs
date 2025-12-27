namespace Marqa.Service.Services.Lessons.Models;

public class CurrentAttendanceStatistics
{
    public int TotalStudentCount { get; set; }
    public int TotalPresentStudentCount { get; set; }
    public int TotalAbsentStudentCount { get; set; }
    public double AttendancePercentage { get; set; }
}