using Marqa.Domain.Enums;

public class UpdatePaymentModel
{
    public int PaymentId { get; set; }
    public int StudentId { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public decimal Amount { get; set; }
    public string Description { get; set; }
    public PaymentOperationType PaymentOperationType { get; set; }
    public decimal CoursePrice { get; set; }
    public int CourseId { get; set; }

}

