using Marqa.Service.Services.Enums.Models;

namespace Marqa.Service.Services.Enums;
public interface IEnumService
{
    public List<EnumGetModel> GetEnumValues<T>() where T : System.Enum;
}
