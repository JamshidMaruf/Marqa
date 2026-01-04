using Marqa.Domain.Enums;

namespace Marqa.Service.Services.Lessons.Models;

public class LessonAttendanceModel
{
    public int LessonId { get; set; }
    public List<StudentInfo> Students { get; set; }

    public class StudentInfo
    {
        public int Id { get; set; }
        public AttendanceStatus Status { get; set; }
        public int? LateTimeInMinutes { get; set; }
    }
}