using System.ComponentModel;

namespace Marqa.Domain.Enums;

public enum StudentFilteringStatus
{
    [Description("Hammasi")]
    All = 0,

    [Description("Aktiv")]
    Active = 1,

    [Description("Yaqinlashayotgan")]
    Upcoming = 2,

    [Description("Tugatgan")]
    Completed = 3,

    [Description("Yopilgan")]
    Closed = 4,

    [Description("Muzlatilgan")]
    Frozen = 5,

    [Description("Guruhsiz")]
    GroupLess = 6,
}
