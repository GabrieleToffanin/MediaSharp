using MediaSharp.Core.Model;

namespace MediaSharp.Core.Pipe.Core
{
    /// <summary>
    /// Builder used withing Dependency injection container initialization.
    /// </summary>
    public interface IHandlerExecutionPipeBuilder
    {
        /// <summary>
        /// Builds with the provided set <see cref="HandlerExecutionPipeStep"/>
        /// </summary>
        /// <returns>An instance of <see cref="HandlerExecutionPipeContext"/></returns>
        HandlerExecutionPipeContext Build();

        /// <summary>
        /// Adds multiple steps at once.
        /// </summary>
        /// <param name="steps">Current <see cref="HandlerExecutionPipeStep"/> to be added</param>
        /// <returns>Current <see cref="IHandlerExecutionPipeBuilder"/></returns>
        IHandlerExecutionPipeBuilder AddSteps(params HandlerExecutionPipeStep[] steps);

        /// <summary>
        /// Adds a single step.
        /// </summary>
        /// <param name="step">Current <see cref="HandlerExecutionPipeStep"/> to be added</param>
        /// <returns>Current <see cref="IHandlerExecutionPipeBuilder"/></returns>
        IHandlerExecutionPipeBuilder AddStep(HandlerExecutionPipeStep step);
    }
}
