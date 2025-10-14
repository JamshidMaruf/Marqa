using Marqa.Domain.Enums;

namespace Marqa.Domain.Entities;

public class PointSystemSetting : Auditable
{
    public string Name { get; set; }
    public int Code { get; set; }
    public int Point {  get; set; }
    public PointHistoryOperation Operation { get; set; }
    public bool IsEnabled { get; set; } 
}