using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaSharp.Core;
using MediaSharp.Core.Attributes;

namespace Tester
{
    [CallableHandler]
    public class NumbersHandler : IRequestHandler<Altro, IEnumerable<Qualcosa>>
    {
        public Task<IEnumerable<Qualcosa>> HandleAsync(Altro request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
