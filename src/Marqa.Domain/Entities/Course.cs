using Marqa.Domain.Enums;

namespace Marqa.Domain.Entities;

public class Course : Auditable
{
    public string Name { get; set; }
    public int LessonCount { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public CourseStatus Status { get; set; }
    public string Level { get; set; }
    public int MaxStudentCount { get; set; }
    public int StudentCount { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public int CompanyId { get; set; }
    public string Subject { get; set; }

    // Navigation
    public Company Company { get; set; }
    public ICollection<CourseWeekday> CourseWeekdays { get; set; }
    public ICollection<CourseTeacher> CourseTeachers { get; set; }
    public ICollection<Lesson> Lessons { get; set; }
    public ICollection<Exam> Exams { get; set; }
    public ICollection<Enrollment> Enrollments { get; set; }
    public ICollection<TeacherAssessment> TeacherAssessments { get; set; }

}