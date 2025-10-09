using System.Reflection;
using Marqa.Domain.Enums;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Marqa.Domain.Entities;

public class Student : Auditable
{ 
    public string StudentAccessId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }   
    public string Phone { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public int CompanyId { get; set; }
    public Company Company { get; set; }
    public StudentDetail StudentDetail { get; set; }    
    public ICollection<Course> Courses { get; set; }
}
