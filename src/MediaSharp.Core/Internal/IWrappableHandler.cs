using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaSharp.Core.Internal
{
    public interface IWrappableHandler
    {
        Task<object> HandleAsync(RequestProxy request, CancellationToken cancellationToken);
    }
}
