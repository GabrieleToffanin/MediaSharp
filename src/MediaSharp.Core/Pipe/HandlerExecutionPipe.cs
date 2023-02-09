using MediaSharp.Core.Pipe.Core;

namespace MediaSharp.Core.Pipe
{
    /// <inheritdoc />
    public class HandlerExecutionPipe : IHandlerExecutionPipe
    {
        /// <inheritdoc />
        public async Task<IRequest<TResult>> ExecutePipeAggregate<TResult>(
            IRequest<TResult> request,
            IHandlerExecutionPipeContext context,
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

            var finalResult = await context.Steps[^1].ExecuteAsync(currentMorphedRequest ?? request, cancellationToken);

            return finalResult;
        }
    }
}
