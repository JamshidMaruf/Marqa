namespace Marqa.Service.Services.EmployeePayments.Models;

public class EmployeePaymentListViewModel
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public EnumViewModel PaymentMethod { get; set; }
    public string PaymentNumber { get; set; }
    public decimal Amount { get; set; }
    public DateTime DateTime { get; set; }
    public EnumViewModel EmployeePaymentOperationType { get; set; }
    public string Description { get; set; } 
}

public class EnumViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
}