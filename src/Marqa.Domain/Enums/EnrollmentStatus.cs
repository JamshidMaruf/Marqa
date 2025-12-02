using System.ComponentModel;

namespace Marqa.Domain.Enums;

public enum EnrollmentStatus
{
    [Description("Foal")]
    Active = 1,

    [Description("Chiqib ketgan")]
    Dropped = 3,

    [Description("Sinov")]
    Test = 4,

    [Description("Tugatgan")]
    Completed = 5,

    [Description("Muzlatilgan")]
    Frozen = 6
}   