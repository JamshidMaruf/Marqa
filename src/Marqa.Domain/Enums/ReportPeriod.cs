using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marqa.Domain.Enums
{
    public enum ReportPeriod
    {
        [Description("Kunlik")]
        Daily,

        [Description("Oylik")]
        Monthly,

        [Description("Yillik")]
        Yearly,

        [Description("Jami")]
        Total
    }
}
