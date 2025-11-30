using System.ComponentModel.DataAnnotations;
using Marqa.Domain.Enums;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Marqa.Domain.Entities;

public class Course : Auditable
{
    public string Name { get; set; }
    public int LessonCount { get; set; }
    public DateOnly StartDate { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public CourseStatus Status { get; set; }
    public int MaxStudentCount { get; set; }
    public int EnrolledStudentCount { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public int CompanyId { get; set; }
    public int SubjectId { get; set; }
    public int TeacherId { get; set; }
    // concurrency token
    public byte[] RowVersion { get; set; }

    // Navigation
    public Company Company { get; set; }
    public Subject Subject { get; set; }
    public Employee Teacher { get; set; }
    public ICollection<CourseWeekday> CourseWeekdays { get; set; }
    public ICollection<Lesson> Lessons { get; set; }
    public ICollection<Exam> Exams { get; set; }
    public ICollection<StudentCourse> StudentCourses { get; set; }
}