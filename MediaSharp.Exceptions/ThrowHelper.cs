using MediaSharp.Exceptions.Common;

namespace MediaSharp.Exceptions
{
    internal static class ThrowHelper
    {
        public static void ThrowRequestHandlerNotFoundException(string message, Exception ex = null)
        {
            throw new RequestHandlerNotFoundException(message, ex);
        }
    }
}
