using MediaSharp.Core.Model;
using MediaSharp.Core.Pipe.Core;

namespace MediaSharp.Core.Pipe
{
    /// <summary>
    /// Defines the context where the steps are
    /// you can run the whole ordered steps by exec
    /// insertion timeline
    /// </summary>
    public class HandlerExecutionPipeContext : IHandlerExecutionPipeContext
    {
        public HandlerExecutionPipeContext(List<HandlerExecutionPipeStep> steps)
        {
            this.Steps = steps;
        }

        public List<HandlerExecutionPipeStep> Steps { get; }
    }
}
