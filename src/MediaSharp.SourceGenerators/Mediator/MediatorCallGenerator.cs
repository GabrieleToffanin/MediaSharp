using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MediaSharp.SourceGenerators.Mediator;

[Generator(LanguageNames.CSharp)]
public partial class MediatorCallGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        //#if DEBUG
        //        if (!Debugger.IsAttached)
        //            Debugger.Launch();
        //#endif

        IncrementalValuesProvider<(HandlerInfoSyntax Left, ConstructorInfo Right)> callableMediatorMethodsInfo =
            context.SyntaxProvider
                .ForAttributeWithMetadataName(
                    "MediaSharp.Core.Attributes.CallableHandlerAttribute",
                    static (node, _) => node is ClassDeclarationSyntax,
                    static (context, _) =>
                    {
                        var isValidClassDecl = (ClassDeclarationSyntax)context.TargetNode;

                        INamedTypeSymbol typeSymbol = (INamedTypeSymbol)context.TargetSymbol;

                        ConstructorInfo ctorInfo = GetCtorInfo(typeSymbol);

                        var infoRetrievalSuccess = Execute.TryGetClassInfo(isValidClassDecl, context, out var namespaceName, out var argumentName, out var containingNamespace);

                        return (
                            new HandlerInfoSyntax
                            {
                                ClassDelc = isValidClassDecl,
                                RequestFull = argumentName,
                                Assembly = namespaceName,
                                Namespace = containingNamespace
                            }, ctorInfo);
                    }).Where(x => x.Item1 is not null);

        context.RegisterSourceOutput(callableMediatorMethodsInfo,
            static (context, item) =>
            {
                CompilationUnitSyntax compilationUnit = Execute.GetKnownClassSyntax(item.Left, item.Right);
                int i = 0;
                context.AddSource($"MediaSharp.SourceGenerated.{item.Left.ClassDelc.Identifier}.g.cs", compilationUnit.GetText(Encoding.UTF8));
                i++;
            });
    }

    private static ConstructorInfo GetCtorInfo(INamedTypeSymbol classDeclSymbol)
    {
        var parameters = new List<ParameterInfo>();

        foreach (IFieldSymbol fieldSymbol in classDeclSymbol.GetMembers().OfType<IFieldSymbol>())
        {
            // Skip fields that are not instance ones (and also ignore generated fields and fixed size buffers)
            if (fieldSymbol is not { IsConst: false, IsStatic: false, IsFixedSizeBuffer: false, IsImplicitlyDeclared: false })
            {
                continue;
            }

            // Track the field normally
            string typeName = fieldSymbol.Type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);

            parameters.Add(new ParameterInfo(fieldSymbol.Name, typeName));
        }

        return new(parameters);
    }
}