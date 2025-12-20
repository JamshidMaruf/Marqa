namespace Marqa.Shared.Services;

/// <summary>
/// Provides environment-related information such as web root path.
/// Replaces static EnvironmentHelper.
/// </summary>
public interface IEnvironmentService
{
    /// <summary>
    /// Gets the absolute path to the web root directory.
    /// </summary>
    string WebRootPath { get; }
}

