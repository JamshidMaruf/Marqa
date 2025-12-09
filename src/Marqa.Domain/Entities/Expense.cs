using Marqa.Domain.Enums;

namespace Marqa.Domain.Entities;

public class Expense : Auditable
{
    public int CompanyId { get; set; }
    public int CategoryId { get; set; }
    public string Description { get; set; }
    public decimal Amount { get; set; }
    public DateTime DateTime { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    
    public Company Company { get; set; }
    public ExpenseCategory Category { get; set; }
}