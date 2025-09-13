using Marqa.Domain.Enums;

namespace Marqa.Service.Services.Teachers.Models;

public class TeacherUpdateModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public TeacherStatus Status { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public DateOnly JoiningDate { get; set; }
    public string Specialization { get; set; }
}
