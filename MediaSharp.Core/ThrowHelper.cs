using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaSharp.Exceptions.Common;

namespace MediaSharp.Core
{
    internal static class ThrowHelper
    {
        public static void ThrowRequestHandlerNotFoundException(string message, Exception ex = null)
        {
            throw new RequestHandlerNotFoundException(message, ex);
        }
    }
}
