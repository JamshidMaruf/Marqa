using System.Text.RegularExpressions;
using Marqa.Shared.Helpers;

namespace Marqa.Teacher.WebApi.Extensions;

public class LowerCaseControllerName : IOutboundParameterTransformer
{
    public string? TransformOutbound(object? value)
    {
        return LowerCaseConverter.Convert(value);
    }
}
