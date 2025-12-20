using Microsoft.AspNetCore.Http;

namespace Marqa.Shared.Services;

/// <summary>
/// Provides access to the current HTTP context and response headers.
/// Replaces static HttpContextHelper.
/// </summary>
public interface IHttpContextService
{
    /// <summary>
    /// Gets the current HTTP context.
    /// </summary>
    HttpContext? HttpContext { get; }

    /// <summary>
    /// Gets the response headers dictionary.
    /// </summary>
    IHeaderDictionary? ResponseHeaders { get; }

    /// <summary>
    /// Adds a header to the response.
    /// </summary>
    void AddResponseHeader(string key, string value);

    /// <summary>
    /// Removes a header to the response.
    /// </summary>
    void RemoveResponseHeader(string key);
}

