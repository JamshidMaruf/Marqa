using System.ComponentModel;

namespace Marqa.Domain.Enums;
public enum HomeTaskStatus
{
    [Description("Biriktirilgan")]
    Assigned = 1,

    [Description("Biriktirilmagan")]
    NotAssigned = 2
}
