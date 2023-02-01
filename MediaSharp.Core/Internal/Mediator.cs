using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MediaSharp.Core.Internal
{
    public sealed partial class Mediator : IMediator
    {
        private readonly MediatorContext _context;

        public Mediator(MediatorContext context)
        {
            this._context = context;
        }
        public async Task<TResult> SendAsync<TResult>(IRequest<TResult> request, CancellationToken cancellationToken)
            where TResult : class
        {
            Type requestType = request.GetType();

            var currentHandlerCall = CollectionsMarshal.GetValueRefOrNullRef(this._context.RequestHandlers, requestType);

            if (currentHandlerCall is null)
            {
                ThrowHelper.ThrowRequestHandlerNotFoundException(
                    $"Can't find an IRequestHandler for type {nameof(request)}");
            }

            var requestProxy = new RequestProxy();
            requestProxy.Proxy = requestProxy.TryGetCasted(ref request);

            return (TResult)await currentHandlerCall.HandleAsync(requestProxy, cancellationToken);
        }


    }
}
