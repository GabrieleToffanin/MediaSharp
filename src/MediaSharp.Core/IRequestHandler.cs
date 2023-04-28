using MediaSharp.Core.Internal;

namespace MediaSharp.Core;

/// <summary>
/// Represents the current declared handler for retrieving <see cref="TResponse"/> type,
/// when executing the declared <seealso cref="HandleAsync"/>
/// method
/// </summary>
/// <typeparam name="TRequest">Current request</typeparam>
/// <typeparam name="TResponse">Current return type</typeparam>
public interface IRequestHandler<in TRequest, TResponse> : IWrappableHandler
    where TRequest : IRequest<TResponse>
    where TResponse : class
{
    /// <summary>
    /// Method will execute request code, returning via Mediator,
    /// the corresponding value
    /// </summary>
    /// <param name="request">Current <see cref="IRequest{TResult}"/> to be handled.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns></returns>
    Task<TResponse> HandleAsync(TRequest request, CancellationToken cancellationToken);
}