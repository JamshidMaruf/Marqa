using Marqa.Domain.Enums;

namespace Marqa.Domain.Entities;

public class Employee : Auditable
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public decimal Salary { get; set; }
    public Gender Gender { get; set; }
    public EmployeeStatus Status { get; set; }
    public DateOnly JoiningDate { get; set; }
    public string Specialization { get; set; }
    public string Info { get; set; }
    public int CompanyId { get; set; }
    public int RoleId { get; set; }
    
    // Navigation
    public Company Company { get; set; }
    public EmployeeRole Role { get; set; }
}

