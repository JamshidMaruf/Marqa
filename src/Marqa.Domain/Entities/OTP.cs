namespace Marqa.Domain.Entities;

public class OTP : Auditable
{
    public string PhoneNumber { get; set; }
    public string Code { get; set; }
    public DateTime ExpiryDate { get; set; }
    public bool IsUsed { get; set; }
}
