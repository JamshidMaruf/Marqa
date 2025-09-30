namespace Marqa.Service.Services.StudentDetails;
namespace Marqa.Service.Services.StudentDetails.Models;

public class StudentDetailViewModel
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public string FatherFirstName { get; set; }
    public string FatherLastName { get; set; }
    public string FatherPhone { get; set; }
    public string MotherFirstName { get; set; }
    public string MotherLastName { get; set; }
    public string MotherPhone { get; set; }
    public string RelativeFirstName { get; set; }
    public string RelativeLastName { get; set; }
    public string RelativePhone { get; set; }
}

