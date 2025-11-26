using Marqa.Domain.Enums;

namespace Marqa.Domain.Entities;

public class Employee : Auditable
{
    public int UserId { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public decimal Salary { get; set; }
    public Gender Gender { get; set; }
    public EmployeeStatus Status { get; set; }
    public DateOnly JoiningDate { get; set; }
    public string Specialization { get; set; }
    public string Info { get; set; }
    public int RoleId { get; set; }
    public int? ParentId { get; set; } // for assistant teacher showing to whom he assists

    // Navigation
    public User User { get; set; }
    public EmployeeRole Role { get; set; }
    public Employee Parent { get; set; }
}