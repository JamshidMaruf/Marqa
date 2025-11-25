namespace Marqa.Domain.Entities;

public class RefreshToken : Auditable
{
    public int UserId { get; set; }
    public string Token { get; set; }
    public DateTime ExpiresAt { get; set; }
    public DateTime? RevokedAt { get; set; }
    public bool IsExpired => DateTime.UtcNow >= ExpiresAt;
    public bool IsRevoked => RevokedAt != null;
    public string CreatedByIp { get; set; }
    public string RevokedByIp { get; set; }
    
    // Navigation Properties
    public User User { get; set; }
}