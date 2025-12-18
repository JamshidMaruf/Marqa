using Marqa.Domain.Enums;

namespace Marqa.Service.Services.Teachers.Models;

public class CalculatedTeacherSalaryModel
{
    public int GroupsCount { get; set; }
    public int ActiveStudentsCount { get; set; }
    public FixedSalary Fixed { get; set; }
    public PercentageSalary Percentage { get; set; }
    public HourlySalary Hourly { get; set; }
    public MixedSalary Mixed { get; set; }
    
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
        public decimal FixSalary { get; set; }
        public int Hours { get; set; }
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