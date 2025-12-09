namespace Marqa.Domain.Entities;

public class TeacherSubject : Auditable
{
    public int TeacherId { get; set; }
    public int SubjectId { get; set; }

    public Teacher Teacher { get; set; }
    public Subject Subject { get; set; }
}
