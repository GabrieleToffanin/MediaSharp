using MediaSharp.Core;
using MediaSharp.Core.Attributes;

namespace Tester;

[CallableHandler]
public partial class BhoHandler : IRequestHandler<Bho, Qualcosa>
{
    public Task<Qualcosa> HandleAsync(Bho request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}