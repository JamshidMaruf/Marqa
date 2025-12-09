using System.ComponentModel;

namespace Marqa.Domain.Enums;

public enum AttendanceStatus
{
    [Description("Hech biri")] None = 0,
    [Description("Kelgan")] Present = 1,
    [Description("Kechikkan")] Late = 2,
    [Description("Kelmagan")] Absent = 3,
    [Description("Sababli")] Excused = 4
}
