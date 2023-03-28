using MediaSharp.Core.Model;
using MediaSharp.Core.Pipe.Core;
using MediaSharp.Core.Pipe.Execution;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MediaSharp.Core.Internal
{
    /// <inheritdoc />
    public sealed class Mediator : IMediator
    {
        /// <summary>
        /// This is the execution pipe, that has the duty of executing
        /// the <see cref="HandlerExecutionPipeStep"/> collection.
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
        public async Task<TResult> SendAsync<TResult>(IRequest<TResult> request, CancellationToken cancellationToken)
            where TResult : class
        {
            return await ExecutePipeline(request, cancellationToken);
        }

        /// <summary>
        /// Gets the current <see cref="IRequest{TResult}"/>
        /// from the mediator and transforms through
        /// the <see cref="IExecutionPipeStep"/>
        /// </summary>
        /// <typeparam name="TResult">Current espected response type</typeparam>
        /// <param name="request">The current <see cref="IRequest{TResult}"/></param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns><see cref="TResult"/></returns>
        private async Task<TResult> ExecutePipeline<TResult>(
            IRequest<TResult> request,
            CancellationToken cancellationToken)
            where TResult : class
        {
            var requestProxy = new RequestProxy(Unsafe.As<IRequest<TResult>, IRequest<object>>(ref request));

            async Task<TResult> Handler() =>
                (TResult)await CollectionsMarshal.GetValueRefOrNullRef(_mediatorContext.RequestHandlers, request.GetType()).HandleAsync(requestProxy, cancellationToken);

            return await this._container.Starter.Aggregate((ExecutionPipeStepDelegate<TResult>)Handler,
                (next, pipeline) =>
                    () => pipeline.ExecutePipelineStep(request, next, cancellationToken))();
        }
    }
}
