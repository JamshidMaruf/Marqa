namespace Marqa.Service.Services.PointSettings.Models;

public class TokenModel
{
    public int PointSettingId { get; set; }
    public int ActivationCount { get; set; }
    public int Point { get; set; }
    public int ExpirationTimeInHours { get; set; }
}
