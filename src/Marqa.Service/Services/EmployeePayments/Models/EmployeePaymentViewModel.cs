using Marqa.Domain.Enums;

namespace Marqa.Service.Services.EmployeePayments.Models;

public class EmployeePaymentViewModel
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public string PaymentNumber { get; set; } 
    public PaymentMethod PaymentMethod { get; set; }
    public EmployeePaymentOperationType EmployeePaymentOperationType { get; set; }
    public decimal Amount { get; set; }
    public DateTime DateTime { get; set; }
    public string Description { get; set; }
}