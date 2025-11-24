using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Marqa.Service.Services.Enum.Models;

namespace Marqa.Service.Services.Enum;
public interface IEnumService
{
    public List<EnumGetModel> GetEnumValues<T>() where T : System.Enum;
}
