using Marqa.Domain.Enums;

namespace Marqa.Service.Services.Teachers.Models;

public class TeacherCreateModel
{
    public int CompanyId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Gender Gender { get; set; }
}
