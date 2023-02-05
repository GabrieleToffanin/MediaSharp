using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using MediaSharp.Common.Pipe.Model;
using MediaSharp.Core;

namespace MediaSharp.Pipeline.Internal
{
    internal class LastExecutionStep : HandlerExecutionPipeStep
    {
        private readonly MediatorContext mediatorContext;
        private readonly IMediator mediator;

        public LastExecutionStep(
            MediatorContext mediatorContext,
            IMediator mediator)
        {
            this.mediatorContext = mediatorContext;
        }

        public override async Task<IRequest<TResult>> ExecuteAsync<TResult>(IRequest<TResult> request, CancellationToken cancellationToken)
        {

            var currentHandler = CollectionsMarshal.GetValueRefOrNullRef(mediatorContext.RequestHandlers, request.GetType());

            return request;
        }
    }
}
