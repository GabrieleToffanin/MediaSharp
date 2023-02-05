using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using MediaSharp.Core.Pipe.Core;
using MediaSharp.Exceptions;
using Microsoft.VisualBasic;

namespace MediaSharp.Core.Internal
{
    public sealed class Mediator : IMediator
    {
        private readonly IHandlerExecutionPipe _executionPipe;
        private readonly IHandlerExecutionPipeContext _context;
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


        public async Task<TResult> SendAsync<TResult>(IRequest<TResult> request, CancellationToken cancellationToken)
            where TResult : class
        {
            return await ExecutePipeline(request, cancellationToken);
        }

        private async Task<TResult> ExecutePipeline<TResult>(IRequest<TResult> request, CancellationToken cancellationToken)
            where TResult : class
        {
            var morhpedRequest = await this._executionPipe.ExecutePipeAggregate(request, _context, cancellationToken);

            return await TryExecute(morhpedRequest, cancellationToken);
        }

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
