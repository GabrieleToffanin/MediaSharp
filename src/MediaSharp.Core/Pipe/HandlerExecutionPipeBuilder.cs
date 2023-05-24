using MediaSharp.Core.Pipe.Core;
using MediaSharp.Core.Pipe.Execution;
using System.Runtime.CompilerServices;

namespace MediaSharp.Core.Pipe;

/// <summary>
/// <inheritdoc cref="IHandlerExecutionPipeBuilder"/>
/// </summary>
public class HandlerExecutionPipeBuilder
{
    private readonly List<IExecutionPipeStep> _steps = new();

    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ExecutionPipeContainer Build()
    {
        return new ExecutionPipeContainer(this._steps);
    }

    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public HandlerExecutionPipeBuilder AddSteps(params IExecutionPipeStep[] steps)
    {
        this._steps.AddRange(steps);

        return this;
    }

    /// <inheritdoc />
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public HandlerExecutionPipeBuilder AddStep(IExecutionPipeStep step)
    {
        this._steps.Add(step);

        return this;
    }
}