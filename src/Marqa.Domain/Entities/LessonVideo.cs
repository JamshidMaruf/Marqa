namespace Marqa.Domain.Entities;

public class LessonVideo : Auditable
{
    public int LessonId { get; set; }
    public int AssetId { get; set; }

    public Lesson Lesson { get; set; }
    public Asset Asset  { get; set; }
}
