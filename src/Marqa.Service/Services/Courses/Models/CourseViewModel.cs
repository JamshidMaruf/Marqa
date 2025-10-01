using Marqa.Domain.Enums;

namespace Marqa.Service.Services.Courses.Models;

public class CourseViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public SubjectInfo Subject { get; set; }
    public TeacherInfo Teacher { get; set; }
    public int LessonCount { get; set; }
    public DateOnly StartDate { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public CourseStatus Status  { get; set; }
    public int MaxStudentCount { get; set; }
    public int AvailableStudentCount { get; set; }
    public string Description { get; set; }
    public List<DayOfWeek> Weekdays { get; set; }
    public List<LessonInfo> Lessons { get; set; }

    public class TeacherInfo
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class SubjectInfo
    {
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
    }

    public class LessonInfo
    {
        public int Id { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public DateOnly Date { get; set; }
        public string Room { get; set; }
    }
}