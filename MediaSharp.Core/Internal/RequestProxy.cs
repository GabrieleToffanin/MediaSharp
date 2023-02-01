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
    public class RequestProxy
    {
        public IRequest<object> Proxy { get; set; }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IRequest<object> TryGetCasted<TResult>(ref IRequest<TResult> current)
            where TResult : class
        {
            return Unsafe.As<IRequest<TResult>,IRequest<object>>(ref current);
        }
    }
}
