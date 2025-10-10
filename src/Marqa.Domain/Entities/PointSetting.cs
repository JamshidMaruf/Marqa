using Marqa.Domain.Enums;

namespace Marqa.Domain.Entities;
public class PointSetting : Auditable
{
    public int Point { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public PointHistoryOperation Operation {get; set;}
    public bool IsEnabled { get; set; } 
    public string? QrCode { get; set; }
}

