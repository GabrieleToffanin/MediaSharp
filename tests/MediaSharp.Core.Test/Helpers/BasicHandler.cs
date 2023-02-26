using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaSharp.Core.Attributes;
using MediaSharp.Core.Internal;

namespace MediaSharp.Core.Test.Helpers
{
    [CallableHandler]
    public partial class BasicHandler : IRequestHandler<BasicRequest, Basic>
    {
        public Task<Basic> HandleAsync(BasicRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
