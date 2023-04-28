namespace MediaSharp.Core.Internal;

/// <summary>
/// Simple readonly struct
/// that serves the purpose of wrapping the current <see cref="IRequest{TResult}"/>
/// into one more generic.
/// </summary>
public readonly record struct RequestProxy(IRequest<object> Proxy)
{
    /// <summary>
    /// Current hold <see cref="Proxy"/>
    /// </summary>
    public IRequest<object> Proxy { get; } = Proxy;
}