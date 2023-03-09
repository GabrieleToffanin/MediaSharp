using MediaSharp.Core.Attributes;

namespace MediaSharp.Core.Test.Helpers;

[CallableHandler]
public partial class BasicHandler : IRequestHandler<BasicRequest, Basic>
{
    public Task<Basic> HandleAsync(BasicRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}