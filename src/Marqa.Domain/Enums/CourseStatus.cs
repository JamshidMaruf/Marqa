using System.ComponentModel;

namespace Marqa.Domain.Enums;

public enum CourseStatus
{
    [Description("Aktiv")]
    Active = 1,
    
    [Description("Kelayotgan")]
    Upcoming = 2,
    
    [Description("Tugallangan")]
    Completed = 3,
    
    [Description("Yopilgan")]
    Closed = 4
}
