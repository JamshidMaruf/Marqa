namespace Marqa.Domain.Entities;

public class StudentHomeTaskFeedback : Auditable
{
    public string FeedBack {  get; set; }
    public int StudentHomeTaskId { get; set; }
    public int TeacherId { get; set; }
    public StudentHomeTask StudentHomeTask{ get; set; } 
    public Employee Teacher {  get; set; }
}
