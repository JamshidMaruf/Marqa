using System.ComponentModel;

namespace Marqa.Domain.Enums;

public enum TeacherStatus
{
    [Description("Faol")] 
    Active = 1,

    [Description("Chiqib ketgan")] 
    Left = 2,

    [Description("Ta'tilda")] 
    OnLeave = 3
}