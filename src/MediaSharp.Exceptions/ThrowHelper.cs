using System.Runtime.CompilerServices;
using MediaSharp.Exceptions.Common;

namespace MediaSharp.Exceptions
{
    public static class ThrowHelper
    {
        public static void ThrowRequestHandlerNotFoundException(string message, Exception ex = null)
        {
            throw new RequestHandlerNotFoundException(message, ex);
        }
    }
}
