using MediaSharp.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaSharp.Core.Internal
{
    public interface IHandlerExecutionPipeContext
    {
        List<HandlerExecutionPipeStep> Steps { get; }
    }
}
