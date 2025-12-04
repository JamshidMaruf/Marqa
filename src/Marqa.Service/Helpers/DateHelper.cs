using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marqa.Shared.Helpers;
public static class DateHelper
{
    public static DateTime ToLocalTimeConverter(DateTime utcdateTime)
    {
        if(utcdateTime.Kind != DateTimeKind.Utc)
        {
            utcdateTime = DateTime.SpecifyKind(utcdateTime, DateTimeKind.Utc);
        }

        DateTime localTime = utcdateTime.ToLocalTime();

        return localTime;
    }
}
