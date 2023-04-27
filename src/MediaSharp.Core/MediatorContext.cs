using MediaSharp.Core.Internal;
using System.Collections.Concurrent;

namespace MediaSharp.Core;

/// <summary>
/// Context used by the source generator for adding the handlers into a collection of them
/// </summary>
public class MediatorContext
{
    public ConcurrentDictionary<Type, IWrappableHandler> RequestHandlers { get; } = new();

    /// <summary>
    /// Adds an handler inside the <see cref="RequestHandlers"/> collection
    /// </summary>
    /// <typeparam name="TRequest">Current <see cref="IRequest{TResult}"/> that can be handled by the <see cref="IRequestHandler{TRequest,TResponse}"/></typeparam>
    /// <typeparam name="TResult">Current <see cref="TResult"/> that will be retrieved by the handler</typeparam>
    /// <param name="handler">Current <see cref="IRequestHandler{TRequest,TResponse}"/> to be used</param>
    public void Add<TRequest, TResult>(IRequestHandler<TRequest, TResult> handler)
        where TRequest : IRequest<TResult>
        where TResult : class
    {
        if (!this.RequestHandlers.ContainsKey(typeof(TRequest)))
            this.RequestHandlers[typeof(TRequest)] = handler;
    }
}