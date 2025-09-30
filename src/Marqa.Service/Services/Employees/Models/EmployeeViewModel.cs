using Marqa.Domain.Enums;

namespace Marqa.Service.Services.Employees.Models;

public class EmployeeViewModel
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public EmployeeStatus Status { get; set; }
    public DateOnly JoiningDate { get; set; }
    public string Specialization { get; set; }
    public string Info { get; set; }
    public int RoleId { get; set; }
    public EmployeeRoleInfo Role { get; set; }

    public class EmployeeRoleInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}