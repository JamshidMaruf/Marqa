namespace Marqa.Domain.Entities;

public class ExpenseCategory : Auditable
{
    public int CompanyId { get; set; }
    public string Name { get; set; }
    
    public Company Company { get; set; }
}