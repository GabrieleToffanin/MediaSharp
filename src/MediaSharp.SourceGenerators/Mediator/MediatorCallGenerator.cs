using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;
using System.Text;

namespace MediaSharp.SourceGenerators.Mediator;

[Generator(LanguageNames.CSharp)]
public partial class MediatorCallGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {

        //#if DEBUG
        //            if (!Debugger.IsAttached)
        //                Debugger.Launch();
        //#endif

        IncrementalValuesProvider<HandlerInfoSyntax> callableMediatorMethodsInfo =
            context.SyntaxProvider
                .ForAttributeWithMetadataName(
                    "MediaSharp.Core.Attributes.CallableHandlerAttribute",
                    static (node, _) => node is ClassDeclarationSyntax,
                    static (context, token) =>
                    {
                        var isValidClassDecl = (ClassDeclarationSyntax)context.TargetNode;

                        var infoRetrievalSuccess = Execute.TryGetClassInfo(isValidClassDecl, context, out var namespaceName, out var argumentName, out var containingNamespace);

                        return infoRetrievalSuccess ? new HandlerInfoSyntax { ClassDelc = isValidClassDecl, RequestFull = argumentName, Assembly = namespaceName, Namespace = containingNamespace } : null;
                    }).Where(x => x is not null);

        context.RegisterSourceOutput(callableMediatorMethodsInfo.Collect(),
            static (context, item) =>
            {
                CompilationUnitSyntax compilationUnit = Execute.GetKnownClassSyntax(item);

                context.AddSource($"MediaSharp.SourceGenerators.Mediator.ContextRegistrations.g.cs", compilationUnit.GetText(Encoding.UTF8));
            });


    }
}