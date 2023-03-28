using MediaSharp.Core.Pipe.Core;

namespace MediaSharp.Core.Pipe.Execution;
public class ExecutionPipeContainer
{
    internal ExecutionPipeContainer(List<IExecutionPipeStep> starter)
    {
        this.Starter = starter;
    }

    internal List<IExecutionPipeStep> Starter { get; }
}
