using Marqa.Domain.Enums;

namespace Marqa.Service.Services.Employees.Models;

public class EmployeeUpdateModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public EmployeeStatus Status { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public DateOnly JoiningDate { get; set; }
    public string Specialization { get; set; }
    public string Info { get; set; }
    public int RoleId { get; set; }
}
