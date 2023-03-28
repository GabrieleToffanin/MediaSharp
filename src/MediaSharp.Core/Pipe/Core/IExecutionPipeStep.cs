using MediaSharp.Core.Model;

namespace MediaSharp.Core.Pipe.Core;

/// <summary>
/// Defines the pipe where the request is sent through
/// you can add steps inside using <see cref="HandlerExecutionPipeStep"/>
/// </summary>
public interface IExecutionPipeStep
{
    /// <summary>
    /// Executes the pipe steps aggregate
    /// until the Handler for the request
    /// </summary>
    /// <typeparam name="TResult">Return type that the ExecutionPipe will have</typeparam>
    /// <param name="request">Request for the <see cref="IRequestHandler{TRequest,TResponse}"/></param>
    /// <param name="next">Next <see cref="HandlerExecutionPipeStep"/> to be executed</param>
    /// <param name="cancellationToken">Cancellation token for the async</param>
    /// <returns>A <see cref="TResult"/></returns>
    Task<TResult> ExecutePipelineStep<TResult>(
        IRequest<TResult> request,
        ExecutionPipeStepDelegate<TResult> next,
        CancellationToken cancellationToken)
        where TResult : class;
}