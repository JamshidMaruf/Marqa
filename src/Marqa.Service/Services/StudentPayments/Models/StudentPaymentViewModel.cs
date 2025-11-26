using Marqa.Domain.Enums;

namespace Marqa.Service.DTOs.StudentPaymentOperations;

public class StudentPaymentViewModel
{
    public int Id { get; set; }
    public int PaymentNumber { get; set; }
    public decimal Amount { get; set; }
    public DateTime DateTime { get; set; }
    public string Description { get; set; }

    public PaymentMethod PaymentMethod { get; set; }
    public PaymentOperationType PaymentOperationType { get; set; }
    public int CourseId { get; set; }
    public decimal CoursePrice { get; set; }
    public int StudentId { get; set; }
}

