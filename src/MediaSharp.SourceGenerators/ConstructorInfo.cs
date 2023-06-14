using System.Collections.Generic;

namespace MediaSharp.SourceGenerators;

internal sealed class ConstructorInfo
{
    internal ConstructorInfo(IEnumerable<ParameterInfo> parameters)
    {
        this.parameters = parameters;
    }

    internal IEnumerable<ParameterInfo> parameters { get; set; }
}
