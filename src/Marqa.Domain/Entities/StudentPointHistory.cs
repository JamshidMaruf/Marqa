using Marqa.Domain.Enums;

namespace Marqa.Domain.Entities;

public class StudentPointHistory : Auditable
{
    public int StudentId { get; set; }
    public int PreviousPoint { get; set; }
    public int GivenPoint {  get; set; }
    public int CurrentPoint { get; set; }
    public string Note { get; set; }
    public PointHistoryOperation Operation { get; set; }

    public Student Student { get; set; }
}