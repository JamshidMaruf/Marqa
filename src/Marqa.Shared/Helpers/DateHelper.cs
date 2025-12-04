using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marqa.Shared.Helpers;
public static class DateHelper
{
    /// <summary>
    /// The method converts a UTC date-time value to local time.
    /// </summary>
    /// <param name="utcdateTime">
    /// The UTC date-time value to be converted.
    /// If the provided date-time is not in UTC format,
    /// The method will first specify it as UTC before conversion.
    /// </param>
    /// <returns>
    /// The method returns the converted local date-time value.
    /// </returns>
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
