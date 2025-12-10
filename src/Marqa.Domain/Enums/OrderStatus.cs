using System.ComponentModel;

namespace Marqa.Domain.Enums;

public enum OrderStatus
{
    [Description("Jarayonda")]
    InProcess,

    [Description("Tayyor")]
    Ready,

    [Description("Berilgan")]
    Given
}
