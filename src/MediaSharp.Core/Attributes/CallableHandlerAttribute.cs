using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaSharp.Core.Attributes
{
    /// <summary>
    /// This attribute is intended to be used on the
    /// Handlers for letting the source generator
    /// create the remaining partial class for the defined
    /// <see cref="IRequestHandler{TRequest,TResponse}"/>
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class CallableHandlerAttribute : Attribute
    {
    }
}
