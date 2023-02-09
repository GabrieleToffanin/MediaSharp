using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaSharp.Core.Internal
{
    /// <summary>
    /// Internal interface, used for wrapping in a more generic
    /// type the current handler
    /// </summary>
    public interface IWrappableHandler
    {
        Task<object> HandleAsync(RequestProxy request, CancellationToken cancellationToken);
    }
}
