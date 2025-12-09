using Marqa.Domain.Enums;

namespace Marqa.Service.Services.Teachers.Models;

public class CalculatedTeacherSalaryModel
{
    public int GroupsCount { get; set; }
    public int StudentsCount { get; set; }
    public decimal? Percent { get; set; }
    public decimal? Amount { get; set; }
    public decimal Total { get; set; }
    public TeacherPaymentType PaymentType { get; set; }
    public List<GroupData> Groups { get; set; }

    public class GroupData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}