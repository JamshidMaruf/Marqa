namespace Marqa.Service.Services.Teachers.Models;

public class TeacherStatistics
{
    public float AttendanceRate { get; set; }
    public float StudentPersistenceRate { get; set; }
    public float LessonsCompletionRate { get; set; }
    public int NewStudentCount { get; set; }
    public int DroppedStudentCount { get; set; }
    public int TotalResult { get; set; }
}
