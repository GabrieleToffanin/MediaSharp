using MediaSharp.Core.Common;
using System.Runtime.CompilerServices;

namespace MediaSharp.Core;

/// <summary>
/// This <see cref="ThrowHelper"/> serves as an helper for throwing exception so the
/// IL produced by the compiler is way cleaner.
/// </summary>
internal static class ThrowHelper
{
    /// <summary>
    /// Throws a new <see cref="RequestHandlerNotFoundException"/>
    /// if the request handler is not found inside the
    /// <see cref="MediatorContext"/> collection
    /// </summary>
    /// <param name="message">Error message</param>
    /// <param name="ex">Current exception</param>
    /// <exception cref="RequestHandlerNotFoundException">Throwed</exception>
    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static void ThrowRequestHandlerNotFoundException(string message, Exception ex = null)
    {
        throw new RequestHandlerNotFoundException(message, ex);
    }
}