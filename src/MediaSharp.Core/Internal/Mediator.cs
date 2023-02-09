using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using MediaSharp.Core.Model;
using MediaSharp.Core.Pipe.Core;
using MediaSharp.Exceptions;
using Microsoft.VisualBasic;

namespace MediaSharp.Core.Internal
{
    /// <inheritdoc />
    public sealed class Mediator : IMediator
    {
        /// <summary>
        /// This is the execution pipe, that has the duty of executing
        /// the <see cref="HandlerExecutionPipeStep"/> collection.
        /// </summary>
        private readonly IHandlerExecutionPipe _executionPipe;

        /// <summary>
        /// This is the context from where the
        /// <see cref="HandlerExecutionPipeStep"/>
        /// get loaded.
        /// </summary>
        private readonly IHandlerExecutionPipeContext _context;

        /// <summary>
        /// From this the Mediator will retrieve the correct handler
        /// for the current <see cref="IRequest{TResult}"/>
        /// </summary>
        private readonly MediatorContext _mediatorContext;

        public Mediator(
            IHandlerExecutionPipeContext context,
            IHandlerExecutionPipe executionPipe,
            MediatorContext mediatorContext)
        {
            this._executionPipe = executionPipe;
            this._context = context;
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
        /// the <see cref="IHandlerExecutionPipe"/>
        /// </summary>
        /// <typeparam name="TResult">Current espected response type</typeparam>
        /// <param name="request">The current <see cref="IRequest{TResult}"/></param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns><see cref="TResult"/></returns>
        private async Task<TResult> ExecutePipeline<TResult>(IRequest<TResult> request, CancellationToken cancellationToken)
            where TResult : class
        {
            var morhpedRequest = await this._executionPipe.ExecutePipeAggregate(request, _context, cancellationToken);

            return await TryExecute(morhpedRequest, cancellationToken);
        }

        /// <summary>
        /// Final step of the Mediator,
        /// is where the transformed request get finally
        /// handled by the corresponding <see cref="IRequestHandler{TRequest,TResponse}"/>
        /// </summary>
        /// <typeparam name="TResult">Expected result Type</typeparam>
        /// <param name="request">Current <see cref="IRequest{TResult}"/></param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A <see cref="TResult"/></returns>
        private async Task<TResult> TryExecute<TResult>(IRequest<TResult> request, CancellationToken cancellationToken)
            where TResult : class
        {
            var requestType = request.GetType();
            var currentHandler = CollectionsMarshal.GetValueRefOrNullRef(_mediatorContext.RequestHandlers, requestType);

            var requestProxy = new RequestProxy(Unsafe.As<IRequest<TResult>, IRequest<object>>(ref request));

            return (TResult)await currentHandler.HandleAsync(requestProxy, cancellationToken);
        }
    }
}
