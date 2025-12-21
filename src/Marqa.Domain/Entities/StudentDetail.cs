namespace Marqa.Domain.Entities;

public class StudentDetail : Auditable
{
    public string FatherFirstName { get; set; }
    public string FatherLastName { get; set; }
    public string FatherPhone { get; set; }
    public string MotherFirstName { get; set; }
    public string MotherLastName { get; set; }
    public string MotherPhone { get; set; }
    public string GuardianFirstName { get; set; }
    public string GuardianLastName { get; set; }
    public string GuardianPhone { get; set; }
    public int StudentId { get; set; }
    public Student Student { get; set; }
}
