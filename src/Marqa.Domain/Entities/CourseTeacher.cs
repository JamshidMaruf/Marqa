namespace Marqa.Domain.Entities;

public class CourseTeacher : Auditable
{
    public int CourseId { get; set; }
    public int TeacherId { get; set; }
    public Course Course { get; set; }
    public Teacher Teacher { get; set; }
}