using System.ComponentModel;

namespace Marqa.Domain.Enums;

public enum StudentFilteringStatus
{
    [Description("Hammasi")]
    All = 0,

    [Description("Aktiv")]
    Active = 1,

    [Description("Tugatgan")]
    Completed = 2,

    [Description("Muzlatilgan")]
    Frozen = 3,

    [Description("Guruhsiz")]
    GroupLess = 4,
}
