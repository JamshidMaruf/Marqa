using Marqa.Domain.Enums;

namespace Marqa.Service.Services.Teachers.Models;

public class CreateTeacherSalaryModel
{
    public int TeacherId { get; set; }
    public int GroupsCount { get; set; }
    public int ActiveStudentsCount { get; set; }
    public decimal TotalSalary { get; set; }
    public decimal Bonus { get; set; }
    public decimal Penalty { get; set; }
    public int Year { get; set; }
    public Month Month { get; set; }
    public List<FixedSalary> FixedSalaries { get; set; } = new();
    public List<PercentageSalary> PercentageSalaries { get; set; } = new();
    public List<HourlySalary> HourlySalaries { get; set; } = new();
    public List<MixedSalary> MixedSalaries { get; set; } = new();
    
    public class FixedSalary
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public int ActiveStudentsCount { get; set; }
        public decimal FixSalary { get; set; }
    }
    
    public class PercentageSalary
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public int ActiveStudentsCount { get; set; }
        public decimal Percent { get; set; }
        public decimal Total { get; set; }
    }
    
    public class HourlySalary
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public int ActiveStudentsCount { get; set; }
        public double Hours { get; set; }
        public decimal Amount { get; set; }
        public decimal Total { get; set; }
    }
    
    public class MixedSalary
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public int ActiveStudentsCount { get; set; }
        public decimal FixSalary { get; set; }
        public decimal Percent { get; set; }
        public decimal Total { get; set; }
    }
}