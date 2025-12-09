using Marqa.Domain.Enums;

namespace Marqa.Domain.Entities;

public class TeacherSalary : Auditable
{
    public int TeacherId { get; set; }
    public decimal Amount { get; set; }
    public decimal? Percent {  get; set; }
    public TeacherPaymentType PaymentType { get; set; }

    // Navigation
    public Teacher Teacher { get; set; }
}
