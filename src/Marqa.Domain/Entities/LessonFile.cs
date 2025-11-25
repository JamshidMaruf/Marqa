namespace Marqa.Domain.Entities;

public class LessonFile : Auditable
{
    public int AssetId { get; set; }
    public int LessonId { get; set; }

    public Asset Asset { get; set; }
    public Lesson Lesson { get; set; }
}
