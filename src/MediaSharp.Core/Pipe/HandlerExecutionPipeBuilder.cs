using MediaSharp.Core.Pipe.Core;
using MediaSharp.Core.Pipe.Execution;

namespace MediaSharp.Core.Pipe;

/// <summary>
/// <inheritdoc cref="IHandlerExecutionPipeBuilder"/>
/// </summary>
public class HandlerExecutionPipeBuilder
{
    private readonly List<IExecutionPipeStep> _steps = new();

    /// <inheritdoc />
    public ExecutionPipeContainer Build()
    {
        return new ExecutionPipeContainer(this._steps);
    }

    /// <inheritdoc />
    public HandlerExecutionPipeBuilder AddSteps(params IExecutionPipeStep[] steps)
    {
        this._steps.AddRange(steps);

        return this;
    }
    /// <inheritdoc />
    public HandlerExecutionPipeBuilder AddStep(IExecutionPipeStep step)
    {
        this._steps.Add(step);

        return this;
    }
}