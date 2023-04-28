namespace MediaSharp.Core;

/// <summary>
/// Represents the main point where mediator will send the 
/// <see cref="IRequest{TResult}"/> to the corresponding <see cref="IRequestHandler{TResponse}"/>
/// </summary>
public interface IMediator
{
    /// <summary>
    /// Fires the query to the corresponding
    /// <see cref="IRequestHandler{TResponse}"/>
    /// </summary>
    /// <typeparam name="TResult">Current return type</typeparam>
    /// <returns>A <see cref="TResult"/></returns>
    Task<TResult> SendAsync<TResult>(IRequest<TResult> request, CancellationToken cancellationToken)
        where TResult : class;
}