namespace MediaSharp.SourceGenerators.Tests.Common;

public interface ITestRepository
{
    Task<TestSomething> GetSomethingAsync();
}