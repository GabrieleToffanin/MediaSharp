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
            var currentHandler = CollectionsMarshal.GetValueRefOrNullRef(this._context.RequestHandlers, request.GetType());

            if (currentHandler is null)
            {
                ThrowHelper.ThrowRequestHandlerNotFoundException(
                    $"Can't find an IRequestHandler for type {nameof(request)}");
            }

            var instance = Unsafe.As<IRequestHandler<IRequest<TResult>, TResult>>(currentHandler);

            return await instance.HandleAsync(request, cancellationToken);
        }


    }
}
