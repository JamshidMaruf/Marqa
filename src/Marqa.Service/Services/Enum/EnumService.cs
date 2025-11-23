using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Marqa.Service.Exceptions;
using Marqa.Service.Services.Enum.Models;

namespace Marqa.Service.Services.Enum;
public class EnumService : IEnumService
{
    public List<EnumGetModel> GetEnumValues<T>() where T : System.Enum
    {
        if (!typeof(T).IsEnum)
            throw new ArgumentIsNotValidException("Argument must be an enum");

        return System.Enum.GetValues(typeof(T))
            .Cast<T>()
            .Select(X => new EnumGetModel
            {
                Id = Convert.ToInt32(X),
                Name = X.ToString()
            })
            .ToList();
    }
}
