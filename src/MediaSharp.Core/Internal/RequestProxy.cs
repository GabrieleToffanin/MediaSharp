using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MediaSharp.Core.Internal
{
    /// <summary>
    /// Simple readonly struct
    /// that serves the purpose of wrapping the current <see cref="IRequest{TResult}"/>
    /// into one more generic.
    /// </summary>
    public readonly struct RequestProxy
    {
        public RequestProxy(IRequest<object> request)
        {
            this.Proxy = request;
        }

        /// <summary>
        /// Current hold <see cref="Proxy"/>
        /// </summary>
        public IRequest<object> Proxy { get; }
    }
}
