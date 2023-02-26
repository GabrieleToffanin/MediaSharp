namespace MediaSharp.Core.Pipe.Core
{
    /// <summary>
    /// Defines the pipe where the request is sent through
    /// you can add steps inside using <see cref="HandlerExecutionPipeStep"/>
    /// </summary>
    public interface IHandlerExecutionPipe
    {
        /// <summary>
        /// Executes the pipe steps aggregate
        /// until the Handler for the request
        /// </summary>
        /// <typeparam name="TResult">Return type that the ExecutionPipe will have</typeparam>
        /// <param name="request">Request for the <see cref="IRequestHandler{TRequest,TResponse}"/></param>
        /// <param name="context">Context where other steps are located
        /// you can modify input through steps and add validation and so on</param>
        /// <param name="cancellationToken">Cancellation token for the async</param>
        /// <returns>A <see cref="TResult"/></returns>
        Task<IRequest<TResult>> ExecutePipeAggregate<TResult>(
            IRequest<TResult> request,
            IHandlerExecutionPipeContext context,
            CancellationToken cancellationToken)
                where TResult : class;
    }
}
