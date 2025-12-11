using Marqa.Domain.Enums;

namespace Marqa.Service.Services.Teachers.Models;

public class TeacherCreateModel
{
    public int CompanyId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Qualiification { get; set; }
    public string Info { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public Gender Gender { get; set; }
    
    public TeacherPaymentType PaymentType { get; set; }
    public decimal Amount { get; set; }
    
    public TeacherType Type{get;set;}
    public TeacherStatus Status{get;set;}
    public DateOnly JoiningDate { get; set; }
    public List<int> SubjectIds { get; set; }
}
