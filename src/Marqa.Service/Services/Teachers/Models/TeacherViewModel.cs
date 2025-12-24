using Marqa.Domain.Enums;

namespace Marqa.Service.Services.Teachers.Models;

public class TeacherViewModel
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public GenderInfo Gender { get; set; }
    public StatusInfo Status { get; set; }
    public DateOnly JoiningDate { get; set; }
    public TeacherPayment Payment { get; set; }
    public string Info { get; set; }
    public string Qualification { get; set; }
    public IEnumerable<CourseInfo> Courses { get; set; }
    public TeacherTypeInfo TypeInfo { get; set; }
}
