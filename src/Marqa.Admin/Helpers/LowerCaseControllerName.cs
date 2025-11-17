using Marqa.Shared.Helpers;

namespace Marqa.Admin.Extensions;

public class LowerCaseControllerName : IOutboundParameterTransformer
{
    public string? TransformOutbound(object? value)
    {
        return LowerCaseConverter.Convert(value);
    }
}
