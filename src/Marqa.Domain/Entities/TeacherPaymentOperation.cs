using Marqa.Domain.Enums;

namespace Marqa.Domain.Entities;

public class TeacherPaymentOperation : Auditable
{
    public int TeacherId { get; set; }
    public string PaymentNumber { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public EmployeePaymentOperationType OperationType { get; set; }
    public decimal Amount { get; set; }
    public DateTime DateTime { get; set; }
    public string Description { get; set; }

    //Navigation
    public Teacher Teacher { get; set; }
}