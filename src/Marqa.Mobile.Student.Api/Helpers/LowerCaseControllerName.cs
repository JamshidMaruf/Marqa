using System.Text.RegularExpressions;
using Marqa.Shared.Helpers;

namespace Marqa.Mobile.Student.Api.Helpers;

public class LowerCaseControllerName : IOutboundParameterTransformer
{
    public string? TransformOutbound(object? value)
    {
        return LowerCaseConverter.Convert(value);
    }
}
