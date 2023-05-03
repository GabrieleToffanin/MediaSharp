namespace MediaSharp.Core;

/// <summary>
/// Represents the default request that <see cref="IMediator"/> will
/// send to the corresponding 
/// </summary>
/// <typeparam name="TResult">Query type return</typeparam>
public interface IRequest<in TResult>
    where TResult : class
{
}