namespace Marqa.Domain.Entities;

public class LessonTeacher : Auditable
{
    public int TeacherId { get; set; }
    public int LessonId { get; set; }

    public Teacher Teacher { get; set; }
    public Lesson Lesson { get; set; }
}
