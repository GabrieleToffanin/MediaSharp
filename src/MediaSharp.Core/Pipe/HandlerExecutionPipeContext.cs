using MediaSharp.Core.Model;
using MediaSharp.Core.Pipe.Core;

namespace MediaSharp.Core.Pipe
{
    /// <summary>
    /// <inheritdoc cref="IHandlerExecutionPipeContext"/>
    /// </summary>
    public class HandlerExecutionPipeContext : IHandlerExecutionPipeContext
    {
        public HandlerExecutionPipeContext(List<HandlerExecutionPipeStep> steps)
        {
            this.Steps = steps;
        }
        /// <inheritdoc />
        public List<HandlerExecutionPipeStep> Steps { get; }
    }
}
