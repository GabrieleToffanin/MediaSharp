using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaSharp.Common.Pipe.Core;
using MediaSharp.Common.Pipe.Model;
using MediaSharp.Core.Internal;

namespace MediaSharp.Pipeline.Internal
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
