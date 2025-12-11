using Marqa.Domain.Enums;

namespace Marqa.Service.Services.Teachers.Models;

public class TeacherUpdateViewModel
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
    public IEnumerable<SubjectInfo> Subjects { get; set; }
    public TeacherTypeInfo TypeInfo { get; set; }

    public class GenderInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class StatusInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class SubjectInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    
    public class TeacherPayment
    {
        public int Id { get; set; }
        public TeacherPaymentType Type { get; set; }
        public string Name{get;set;}
        public decimal? FixSalary { get; set; }
        public decimal? SalaryPercentPerStudent {  get; set; }
        public decimal? SalaryAmountPerHour { get; set; }
    }

    public class TeacherTypeInfo
    {
        public int Id { get; set; }
        public string Type { get; set; }
    }
}
