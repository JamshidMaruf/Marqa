using System;

namespace Marqa.Domain.Entities;

public class HomeTask : Auditable
{
    public int LessonId { get; set; }
    public DateTime Deadline { get; set; }
    public string Description { get; set; }

    // Navigation
    public Lesson Lesson { get; set; }
    public HomeTaskFile HomeTaskFile { get; set; }
}
public class HomeTaskFile : Auditable
{
    public string FileName { get; set; }
    public string FilePath { get; set; }
    public int? HomeTaskId { get; set; }
    public HomeTask HomeTask { get; set; }
}
