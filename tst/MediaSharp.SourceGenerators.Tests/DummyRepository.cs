using MediaSharp.SourceGenerators.Tests.Common;

namespace MediaSharp.SourceGenerators.Tests;

public class DummyRepository : ITestRepository
{
    public async Task<TestSomething> GetSomethingAsync()
    {
        return new TestSomething("MediaSharp !");
    }
}