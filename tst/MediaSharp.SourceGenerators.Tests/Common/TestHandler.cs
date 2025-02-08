using MediaSharp.Core;
using MediaSharp.Core.Attributes;

namespace MediaSharp.SourceGenerators.Tests.Common;

[CallableHandler]
public partial class TestHandler : IRequestHandler<TestRequest, TestSomething>
{
    private readonly ITestRepository _testRepository;
    
    public async Task<TestSomething> HandleAsync(TestRequest request, CancellationToken cancellationToken)
    {
        return await _testRepository.GetSomethingAsync();
    }
}