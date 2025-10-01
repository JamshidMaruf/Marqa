using Marqa.Domain.Enums;

namespace Marqa.Service.Services.Lessons.Models;

public class LessonAttendanceModel
{
    public int LessonId { get; set; }
    public int StudentId { get; set; }
    public AttendanceStatus Status { get; set; }
}