using MediaSharp.Core.Model;

namespace MediaSharp.Core.Pipe.Core
{
    public interface IHandlerExecutionPipeBuilder
    {
        HandlerExecutionPipeContext Build();

        IHandlerExecutionPipeBuilder AddSteps(params HandlerExecutionPipeStep[] steps);

        IHandlerExecutionPipeBuilder AddStep(HandlerExecutionPipeStep step);
    }
}
