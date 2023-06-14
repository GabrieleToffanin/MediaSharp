using MediaSharp.Core;
using MediaSharp.Core.Attributes;
using Tester.Foooooooo.Qualcosa;
using Tester.Request.Cristo;

namespace Tester;

[CallableHandler]
public partial class BhoHandler : IRequestHandler<Bho, Qualcosa>
{
    private readonly IFoo foo;

    public async Task<Qualcosa> HandleAsync(Bho request, CancellationToken cancellationToken)
    {
        return await Task.Run(() => new Qualcosa() { Id = 1 });
    }
}