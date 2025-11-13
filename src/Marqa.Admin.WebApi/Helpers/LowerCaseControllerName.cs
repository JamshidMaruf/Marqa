using Marqa.Shared.Helpers;

namespace Marqa.Admin.WebApi.Extensions;

public class LowerCaseControllerName : IOutboundParameterTransformer
{
    public string? TransformOutbound(object? value)
    {
        return LowerCaseConverter.Convert(value);
    }
}
