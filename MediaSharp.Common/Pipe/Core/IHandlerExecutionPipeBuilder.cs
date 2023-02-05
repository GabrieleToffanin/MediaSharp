using MediaSharp.Common.Pipe.Model;
using MediaSharp.Core.Internal;

namespace MediaSharp.Common.Pipe.Core
{
    public interface IHandlerExecutionPipeBuilder
    {
        HandlerExecutionPipeContext Build();

        IHandlerExecutionPipeBuilder AddSteps(params HandlerExecutionPipeStep[] steps);

        IHandlerExecutionPipeBuilder AddStep(HandlerExecutionPipeStep step);
    }
}
