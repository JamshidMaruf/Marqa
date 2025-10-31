namespace Marqa.Domain.Entities;

public class ExamSettingItem : Auditable
{
    public int ExamSettingId { get; set; }
    public float Score { get; set; }
    public int GivenPoints { get; set; }
    
    public ExamSetting Setting { get; set; }
}