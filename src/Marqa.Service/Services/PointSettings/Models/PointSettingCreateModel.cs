using Marqa.Domain.Enums;

namespace Marqa.Service.Services.PointSettings.Models;
public class PointSettingCreateModel
{
    public int Point { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public PointHistoryOperation Operation { get; set; }
}

public class PointSettingUpdateModel
{
    public int Point { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public PointHistoryOperation Operation { get; set; }
}

public class PointSettingViewModel
{
    public int Point { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public PointHistoryOperation Operation { get; set; }
}

public class TokenModel
{
    public int PointSettingId { get; set; }
    public int ActivationCount { get; set; }
    public int Point { get; set; }
    public DateTime ExpireAt { get; set; }
}
