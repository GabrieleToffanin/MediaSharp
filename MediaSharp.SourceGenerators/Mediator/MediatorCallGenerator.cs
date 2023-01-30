using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MediaSharp.SourceGenerators.Mediator
{
    [Generator(LanguageNames.CSharp)]
    public partial class MediatorCallGenerator : IIncrementalGenerator
    {
        public void Initialize(IncrementalGeneratorInitializationContext context)
        {

//#if DEBUG
//            if (!Debugger.IsAttached)
//                Debugger.Launch();
//#endif

            IncrementalValuesProvider<(ClassDeclarationSyntax, IAssemblySymbol)> callableMediatorMethodsInfo =
                context.SyntaxProvider
                    .ForAttributeWithMetadataName(
                        "MediaSharp.Core.Attributes.CallableHandlerAttribute",
                        static (node, _) => node is ClassDeclarationSyntax,
                        static (context, token) =>
                        {
                            ClassDeclarationSyntax isValidClassCdecl =
                                (ClassDeclarationSyntax)context.TargetNode;

                            var isValidClassDecl = Execute.TryGetClassInfo(isValidClassCdecl, context, out var namespaceName);

                            return isValidClassDecl ? (ClassDecl: isValidClassCdecl,Namespace: namespaceName) : (null, null);
                        }).Where(x => x is not (null, null));

            context.RegisterSourceOutput(callableMediatorMethodsInfo.Collect(),
                static (context, item) =>
                {
                    CompilationUnitSyntax compilationUnit = Execute.GetKnownClassSyntax(item);

                    context.AddSource($"MediaSharp.SourceGenerators.Mediator.ContextRegistrations.g.cs", compilationUnit.GetText(Encoding.UTF8));
                });

            
        }
    }
}
