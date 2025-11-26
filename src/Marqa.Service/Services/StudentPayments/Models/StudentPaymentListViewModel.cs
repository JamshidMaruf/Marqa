namespace Marqa.Service.DTOs.StudentPaymentOperations;

public class StudentPaymentListViewModel
{
    public int StudentId { get; set; }
    public EnumViewModel PaymentMethod { get; set; }
    public decimal Amount { get; set; }
    public DateTime DateTime { get; set; }
    public EnumViewModel PaymentOperationType { get; set; }
    public string CourseName { get; set; }
}
