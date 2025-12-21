using Marqa.Domain.Enums;

namespace Marqa.Domain.Entities;

public class TeacherAssessment : Auditable
{
    public int TeacherId { get; set; }
    public int StudentId { get; set; }
    public int CourseId { get; set; }
    public Rate Rating { get; set; }
    public string Description { get; set; }
    public DateTime SubmittedDateTime { get; set; }

    public Teacher Teacher { get; set; }
    public Student Student { get; set; }
    public Course Course { get; set; }
    public Rate Rate { get; set; }
}