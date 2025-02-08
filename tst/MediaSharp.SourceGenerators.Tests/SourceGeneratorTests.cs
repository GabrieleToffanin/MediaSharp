using MediaSharp.Core;
using MediaSharp.SourceGenerators.Tests.Common;

namespace MediaSharp.SourceGenerators.Tests;

public class SourceGeneratorTests : SourceGeneratorFixture
{
    private readonly TestHandler testHandler;

    public SourceGeneratorTests()
    {
        var dummyRepo = new DummyRepository();
    }
    
    [Fact]
    public async Task SourceGeneratorCorrectlyImplementsWrapperMethod()
    {
        var request = new TestRequest();
        
        var result = await Mediator.SendAsync(request, CancellationToken.None);
        
        Assert.Equal("MediaSharp !", result.Name);
    }
}