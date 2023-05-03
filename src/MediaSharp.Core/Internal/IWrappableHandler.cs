using System.ComponentModel;

namespace MediaSharp.Core.Internal;

/// <summary>
/// Internal interface, used for wrapping in a more generic
/// type the current handler
/// </summary>
[Obsolete("This is not intended to be used directly to the user - Source Generator needs this.")]
[EditorBrowsable(EditorBrowsableState.Never)]
public interface IWrappableHandler
{
    Task<object> HandleAsync(IRequest<object> request, CancellationToken cancellationToken);
}