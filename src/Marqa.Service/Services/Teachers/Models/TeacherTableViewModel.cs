namespace Marqa.Service.Services.Teachers.Models;

public class TeacherTableViewModel
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public DateOnly JoiningDate { get; set; }
    public string Qualification { get; set; }
    public StatusInfo Status { get; set; }
    public TeacherTypeInfo Type { get; set; }
    public List<CourseInfo> Courses { get; set; }
}