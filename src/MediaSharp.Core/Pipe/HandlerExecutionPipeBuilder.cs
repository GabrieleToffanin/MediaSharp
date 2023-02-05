using MediaSharp.Core.Model;
using MediaSharp.Core.Pipe.Core;

namespace MediaSharp.Core.Pipe
{
    public class HandlerExecutionPipeBuilder : IHandlerExecutionPipeBuilder
    {
        private readonly List<HandlerExecutionPipeStep> _steps = new();

        public HandlerExecutionPipeContext Build()
        {
            return new HandlerExecutionPipeContext(_steps);
        }

        public IHandlerExecutionPipeBuilder AddSteps(params HandlerExecutionPipeStep[] steps)
        {
            _steps.AddRange(steps);

            return this;
        }

        public IHandlerExecutionPipeBuilder AddStep(HandlerExecutionPipeStep step)
        {
            _steps.Add(step);

            return this;
        }
    }
}
