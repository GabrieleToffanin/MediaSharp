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
        internal IAssemblySymbol Assembly { get; set; }
        internal INamespaceSymbol Namespace { get; set; }
        internal string RequestFull { get; set; }
    }
}
