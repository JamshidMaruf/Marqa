namespace Marqa.Domain.Entities;

public class LessonVideo : Auditable
{
    public string VideoName { get; set; }
    public string VideoPath { get; set; }
    public int LessonId { get; set; }

    public Lesson Lesson { get; set; }
}
