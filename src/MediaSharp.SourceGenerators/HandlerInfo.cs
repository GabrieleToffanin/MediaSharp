using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MediaSharp.SourceGenerators
{
    internal class HandlerInfoSyntax
    {
        internal ClassDeclarationSyntax ClassDelc { get; set; }
        internal IAssemblySymbol assemblyInfo { get; set; }
        internal string argumentName { get; set; }
    }
}
