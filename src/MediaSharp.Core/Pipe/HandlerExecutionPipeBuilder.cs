using MediaSharp.Core.Model;
using MediaSharp.Core.Pipe.Core;

namespace MediaSharp.Core.Pipe
{
    /// <summary>
    /// <inheritdoc cref="IHandlerExecutionPipeBuilder"/>
    /// </summary>
    public class HandlerExecutionPipeBuilder : IHandlerExecutionPipeBuilder
    {
        private readonly List<HandlerExecutionPipeStep> _steps = new();

        /// <inheritdoc />
        public HandlerExecutionPipeContext Build()
        {
            return new HandlerExecutionPipeContext(_steps);
        }
        /// <inheritdoc />
        public IHandlerExecutionPipeBuilder AddSteps(params HandlerExecutionPipeStep[] steps)
        {
            _steps.AddRange(steps);

            return this;
        }
        /// <inheritdoc />
        public IHandlerExecutionPipeBuilder AddStep(HandlerExecutionPipeStep step)
        {
            _steps.Add(step);

            return this;
        }
    }
}
