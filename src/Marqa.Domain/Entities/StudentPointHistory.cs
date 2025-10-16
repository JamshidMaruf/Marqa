using Marqa.Domain.Enums;

namespace Marqa.Domain.Entities;
<<<<<<< HEAD

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
=======
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
>>>>>>> e5c696dbdfa0f966bffc15b8a85fcd1f41526ada
