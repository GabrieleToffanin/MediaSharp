using MediaSharp.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaSharp.Core.Pipe.Core
{
    /// <summary>
    /// This is the context for the handlers pipeline
    /// that holds all the <see cref="HandlerExecutionPipeStep"/>
    /// defined by the user/default
    /// </summary>
    public interface IHandlerExecutionPipeContext
    {
        /// <summary>
        /// This is a collection of the steps that will be executed.<br></br>
        /// The execution order will be based on the
        /// insertion order.
        /// </summary>
        List<HandlerExecutionPipeStep> Steps { get; }
    }
}
