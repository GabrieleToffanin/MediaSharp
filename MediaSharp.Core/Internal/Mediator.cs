using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaSharp.Core.Internal
{
    public sealed partial class Mediator : IMediator
    {
        public async Task<TResult> SendAsync<TResult>(IRequest<TResult> request, CancellationToken cancellationToken)
            where TResult : class
        {
            ThrowHelper.ThrowRequestHandlerNotFoundException($"Can't find an IRequestHandler for type {nameof(request)}");

            return null;
        }


    }
}
