namespace Marqa.Domain.Entities;

public class Basket : Auditable
{
    public int StudentId { get; set; }
    public int TotalPrice { get; set; }
    
    public Student Student { get; set; }
}