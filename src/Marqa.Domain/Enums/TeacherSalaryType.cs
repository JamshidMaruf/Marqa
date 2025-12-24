using System.ComponentModel;

namespace Marqa.Domain.Enums;

public enum TeacherSalaryType
{
    [Description("Qat'iy oylik")]
    Fixed = 1, 

    [Description("Foiz (o'quvchilardan)")]
    Percentage = 2,

    [Description("Soatlik")]
    Hourly = 3,

    [Description("Aralash (oylik + foiz")]
    Mixed = 4   
}