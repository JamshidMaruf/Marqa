using Marqa.Domain.Enums;

namespace Marqa.Service.Services.Lessons.Models;

public class StudentAttendanceModel
{
    public int StudentId { get; set; }
    public string StudentName { get; set; }
    public AttendanceStatus Status { get; set; }
    public int LateTimeInMinutes { get; set; }
}