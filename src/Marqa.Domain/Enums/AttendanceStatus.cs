using System.ComponentModel;

namespace Marqa.Domain.Enums;

public enum AttendanceStatus
{
    [Description("Davomat qilinmagan")] None = 0,
    [Description("Kelgan")] Present = 1,
    [Description("Kechikkan")] Late = 2,
    [Description("Kelmagan")] Absent = 3,
    [Description("Sababli")] Excused = 4,
    [Description("Muzlatilgan")] Frozen = 5
}
