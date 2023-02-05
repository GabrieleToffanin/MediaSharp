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
    public struct RequestProxy
    {
        public RequestProxy(IRequest<object> request)
        {
            this.Proxy = request;
        }

        public IRequest<object> Proxy { get; set; }
    }
}
