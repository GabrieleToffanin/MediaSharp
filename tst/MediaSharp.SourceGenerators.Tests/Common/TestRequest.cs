using MediaSharp.Core;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;

namespace MediaSharp.SourceGenerators.Tests.Common;

public record TestRequest : IRequest<TestSomething>;

public record TestSomething(string Name);