using Marqa.Domain.Enums;

namespace Marqa.Domain.Entities;
public class StudentPointHistory:Auditable
{
    public int StudentId { get; set; }
    public int PreviousPoint {  get; set; }
    public int Givenpoint { get; set; }
    public int CurrentPoint {  get; set; }
    public string Note {  get; set; }
    public PointHistoryOperation Opeartion { get; set; }

    public Student Student { get; set; }
}
