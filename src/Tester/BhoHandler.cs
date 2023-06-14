using MediaSharp.Core;
using MediaSharp.Core.Attributes;

namespace Tester;

[CallableHandler]
public partial class BhoHandler : IRequestHandler<Bho, Qualcosa>
{
    public async Task<Qualcosa> HandleAsync(Bho request, CancellationToken cancellationToken)
    {
        return await Task.Run(() => new Qualcosa() { Id = 1 });
    }
}