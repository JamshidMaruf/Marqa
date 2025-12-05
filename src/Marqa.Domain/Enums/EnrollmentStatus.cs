using System.ComponentModel;

namespace Marqa.Domain.Enums;

public enum EnrollmentStatus
{
    [Description("Foal")]
    Active = 1,

    [Description("Chiqib ketgan")]
    Dropped = 2,

    [Description("Sinov")]
    Test = 3,

    [Description("Tugatgan")]
    Completed = 4,

    [Description("Muzlatilgan")]
    Frozen = 5
}   