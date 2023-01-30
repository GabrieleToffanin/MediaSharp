using MediaSharp.Core;
using MediaSharp.Core.Attributes;

[CallableHandler]
public partial class BhoHandler : IRequestHandler<Bho, Qualcosa>
{
    public Task<Qualcosa> HandleAsync(Bho request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}