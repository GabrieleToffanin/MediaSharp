using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using MediaSharp.Common.Pipe.Core;
using MediaSharp.Core;
using MediaSharp.Core.Internal;

namespace MediaSharp.Pipeline.Internal
{
    public class HandlerExecutionPipe : IHandlerExecutionPipe
    {
        public async Task<IRequest<TResult>> ExecutePipeAggregate<TResult>(
            IRequest<TResult> request,
            HandlerExecutionPipeContext context,
            CancellationToken cancellationToken) where TResult : class
        {
            IRequest<TResult> currentMorphedRequest = default;

            for (int i = 0; i < context.Steps.Count - 1; i++)
            {

                if (currentMorphedRequest != null)
                    currentMorphedRequest =
                        await context.Steps[i].ExecuteAsync(currentMorphedRequest, cancellationToken);

                else currentMorphedRequest = await context.Steps[i].ExecuteAsync(request, cancellationToken);
            }

            return await context.Steps[^1].ExecuteAsync(currentMorphedRequest, cancellationToken);
        }
    }
}
