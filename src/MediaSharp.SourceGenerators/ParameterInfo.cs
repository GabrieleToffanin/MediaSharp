namespace MediaSharp.SourceGenerators;
internal sealed record ParameterInfo
{
    public ParameterInfo(string name, string type)
    {
        Name = name;
        Type = type;
    }

    internal string Name { get; set; }
    internal string Type { get; set; }
}
