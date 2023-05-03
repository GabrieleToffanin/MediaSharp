using MediaSharp.Core.Model;
using MediaSharp.Core.Pipe.Core;
using MediaSharp.Core.Pipe.Execution;
using System.Runtime.CompilerServices;


#pragma warning disable CS0618

namespace MediaSharp.Core.Internal;

/// <inheritdoc />
internal sealed class Mediator : IMediator
{
    /// <summary>
    /// This is the execution pipe, that has the duty of executing
    /// the <see cref="ExecutionPipeContainer"/> collection.
    /// </summary>
    private readonly ExecutionPipeContainer _container;

    /// <summary>
    /// From this the Mediator will retrieve the correct handler
    /// for the current <see cref="IRequest{TResult}"/>
    /// </summary>
    private readonly MediatorContext _mediatorContext;

    public Mediator(
        ExecutionPipeContainer container,
        MediatorContext mediatorContext)
    {
        this._container = container;
        this._mediatorContext = mediatorContext;
    }

    /// <inheritdoc />
    public async Task<TResult> SendAsync<TResult>(
        IRequest<TResult> request,
        CancellationToken cancellationToken)
            where TResult : class
    {
        var requestType = request.GetType();

        var exists = this._mediatorContext.RequestHandlers.TryGetValue(requestType, out var handler);

        if (!exists)
            handler = this._mediatorContext.Resolve<TResult>(requestType);

        var proxy = Unsafe.As<IRequest<TResult>, IRequest<object>>(ref request);

        if (this._container is { Starter.Count: > 0 })
            return await this.ExecutePipeline(request, handler!, proxy, cancellationToken);

        return (await handler!.HandleAsync(proxy, cancellationToken) as TResult)!;
    }

    /// <summary>
    /// Gets the current <see cref="IRequest{TResult}"/>
    /// from the mediator and transforms through
    /// the <see cref="IExecutionPipeStep"/>
    /// </summary>
    /// <typeparam name="TResult">Current expected response type</typeparam>
    /// <param name="request">The current <see cref="IRequest{TResult}"/></param>
    /// <param name="handler">Pre calculated <see cref="IRequestHandler{TRequest,TResponse}"/></param>
    /// <param name="requestStarter">Pre calculated <see cref="IRequest{TResult}"/></param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns><see cref="TResult"/></returns>
    [MethodImpl(MethodImplOptions.NoInlining)]
    private async Task<TResult> ExecutePipeline<TResult>(
        IRequest<TResult> request,
        IWrappableHandler handler,
        IRequest<object> requestStarter,
        CancellationToken cancellationToken)
        where TResult : class
    {
        async Task<TResult> Handler() =>
         await handler.HandleAsync(requestStarter, cancellationToken) as TResult;

        return await this._container.Starter.Aggregate(
            (ExecutionPipeStepDelegate<TResult>)Handler,
            (next, pipeline) =>
                () => pipeline.ExecutePipelineStep(request, next, cancellationToken))();
    }
}