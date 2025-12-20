namespace Marqa.Service.Services;

/// <summary>
/// Marker interface for automatic service registration with Scrutor.
/// All services implementing this interface will be automatically registered as Scoped.
/// </summary>
public interface IScopedService
{
}

/// <summary>
/// Marker interface for automatic service registration with Scrutor.
/// All services implementing this interface will be automatically registered as Singleton.
/// </summary>
public interface ISingletonService
{
}

/// <summary>
/// Marker interface for automatic service registration with Scrutor.
/// All services implementing this interface will be automatically registered as Transient.
/// </summary>
public interface ITransientService
{
}

