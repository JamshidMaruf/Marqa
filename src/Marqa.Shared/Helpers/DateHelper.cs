using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marqa.Shared.Helpers;
public static class DateHelper
{
    /// <summary>
    /// UTC vaqtni lokal vaqtga o'zgartiradi.
    /// </summary>
    /// <param name="utcdateTime">
    /// UTC farmatda bolishi kerak bolgan sana va vaqt qiymati.
    /// Agar DateTimeKind noto‘g‘ri bo‘lsa, majburan Utc
    /// turiga o‘tkaziladi.
    /// </param>
    /// <returns>
    /// Mahalliy vaqt (Local Time) ko‘rinishiga aylantirilgan sana-vaqt.
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
