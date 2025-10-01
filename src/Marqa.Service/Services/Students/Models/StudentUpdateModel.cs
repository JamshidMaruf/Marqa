using Marqa.Domain.Enums;

namespace Marqa.Service.Services.Students.Models;

public class StudentUpdateModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }

}
