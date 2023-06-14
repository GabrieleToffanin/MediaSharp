using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace MediaSharp.SourceGenerators.Mediator;

internal static class Execute
{
    public static bool TryGetClassInfo(
        ClassDeclarationSyntax classSyntax,
        GeneratorAttributeSyntaxContext ctx,
        out IAssemblySymbol assemblySymbol,
        out string argumentName,
        out INamespaceSymbol containingNa)
    {
        var classSymbol = ModelExtensions.GetDeclaredSymbol(ctx.SemanticModel, classSyntax) as ITypeSymbol;

        argumentName = "";

        var isClassMediaRegistrable =
            classSymbol.AllInterfaces.Any(x => x.Name is "IRequestHandler");

        if (isClassMediaRegistrable)
        {
            var currentTypeTarget = classSymbol.AllInterfaces[0].TypeArguments[0];
            var fullArgumentNamespace = currentTypeTarget.ContainingNamespace.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
            var fullArgumentName = currentTypeTarget.Name;

            var separatorToken = string.IsNullOrEmpty(fullArgumentNamespace) ? "" : ".";

            argumentName = $"{fullArgumentNamespace}{separatorToken}{fullArgumentName}";
        }

        assemblySymbol = classSymbol.ContainingAssembly;
        containingNa = classSymbol.ContainingNamespace;

        return isClassMediaRegistrable;
    }

    public static CompilationUnitSyntax GetKnownClassSyntax(HandlerInfoSyntax item, ConstructorInfo ctorInfo)
    {
        var memberDeclSyntax = new SyntaxList<MemberDeclarationSyntax>();
        var ctorSyntax = CreateConstuctorBased(item.ClassDelc, ctorInfo);

        var decl =
            NamespaceDeclaration(
                IdentifierName(item.Namespace.OriginalDefinition.ToString()))
                .WithMembers(
                SingletonList<MemberDeclarationSyntax>(
                    ClassDeclaration(item.ClassDelc.Identifier)
                        .WithModifiers(
                            TokenList(
                                new[]
                                {
                                        Token(SyntaxKind.PublicKeyword),
                                        Token(SyntaxKind.PartialKeyword)
                                }))
                        .AddMembers(ctorSyntax, CreateMethodImplSyntax(item.RequestFull))));

        memberDeclSyntax = memberDeclSyntax.Add(decl);

        return CompilationUnit()
            .AddMembers(memberDeclSyntax.ToArray())
            .NormalizeWhitespace();
    }

    private static ArgumentListSyntax CreateConstructorInitArgSyntax(ParameterListSyntax pls)
    {
        var singlList = new SeparatedSyntaxList<ArgumentSyntax>();

        foreach (var param in pls.Parameters)
        {
            singlList = singlList.Add(Argument(
                IdentifierName(param.Identifier)));
        }

        return ArgumentList(singlList);
    }

    private static ParameterSyntax CreateMediaSharpContextInjection()
    {
        return Parameter(
                Identifier("context"))
            .WithType(
                IdentifierName("MediaSharp.Core.MediatorContext"));
    }


    private static ConstructorDeclarationSyntax CreateConstuctorBased(ClassDeclarationSyntax classSymbol, ConstructorInfo ctorInfo)
    {
        AttributeListSyntax[] attributes =
        {
            AttributeList(SingletonSeparatedList(
                Attribute(IdentifierName("global::System.CodeDom.Compiler.GeneratedCode")).AddArgumentListArguments(
                    AttributeArgument(LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(typeof(MediatorCallGenerator).FullName))),
                    AttributeArgument(LiteralExpression(SyntaxKind.StringLiteralExpression, Literal(typeof(MediatorCallGenerator).Assembly.GetName().Version.ToString())))))),
            AttributeList(SingletonSeparatedList(Attribute(IdentifierName("global::System.Diagnostics.DebuggerNonUserCode")))),
            AttributeList(SingletonSeparatedList(Attribute(IdentifierName("global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage"))))
        };

        var constructorArgumentList = ctorInfo.parameters.Select(field => Parameter(Identifier(field.Name)).WithType(IdentifierName(field.Type))).ToList();

        constructorArgumentList.Add(CreateMediaSharpContextInjection());

        return ConstructorDeclaration(
                classSymbol.Identifier)
            .WithModifiers(
                TokenList(
                    Token(SyntaxKind.PublicKeyword)))
            .AddParameterListParameters(constructorArgumentList.ToArray())
            .WithBody(
                Block(
                    SingletonList<StatementSyntax>(
                        ExpressionStatement(
                            InvocationExpression(
                                    MemberAccessExpression(
                                        SyntaxKind.SimpleMemberAccessExpression,
                                        IdentifierName("context"),
                                        IdentifierName("Add")))
                                .WithArgumentList(
                                    ArgumentList(
                                        SingletonSeparatedList<ArgumentSyntax>(
                                            Argument(
                                                ThisExpression())))))))).AddAttributeLists(attributes);
    }

    private static MethodDeclarationSyntax CreateMethodImplSyntax(string classArgumentName)
    {
        return MethodDeclaration
            (
                GenericName
                    (
                        Identifier("Task"))
                    .WithTypeArgumentList
                    (
                        TypeArgumentList
                        (
                            SingletonSeparatedList<TypeSyntax>
                            (
                                PredefinedType
                                (
                                    Token(SyntaxKind.ObjectKeyword))))),
                Identifier("HandleAsync"))
            .WithModifiers
            (
                TokenList
                (
                    new[]
                    {
                        Token(SyntaxKind.PublicKeyword),
                        Token(SyntaxKind.AsyncKeyword)
                    }))
            .WithParameterList
            (
                ParameterList
                (
                    SeparatedList<ParameterSyntax>
                    (
                        new SyntaxNodeOrToken[]
                        {
                            Parameter
                                (
                                    Identifier("request"))
                                .WithType
                                (
                                    IdentifierName("MediaSharp.Core.IRequest<object>")),
                            Token(SyntaxKind.CommaToken),
                            Parameter
                                (
                                    Identifier("cancellationToken"))
                                .WithType
                                (
                                    IdentifierName("CancellationToken"))
                        })))
            .WithBody
            (
                Block
                (
                    SingletonList<StatementSyntax>
                    (
                        ReturnStatement
                        (
                            AwaitExpression
                            (
                                InvocationExpression
                                    (
                                        MemberAccessExpression
                                        (
                                            SyntaxKind.SimpleMemberAccessExpression,
                                            ThisExpression(),
                                            IdentifierName("HandleAsync")))
                                    .WithArgumentList
                                    (
                                        ArgumentList
                                        (
                                            SeparatedList<ArgumentSyntax>
                                            (
                                                new SyntaxNodeOrToken[]
                                                {
                                                    Argument
                                                    (
                                                        BinaryExpression
                                                        (
                                                            SyntaxKind.AsExpression,
                                                            IdentifierName("request"),
                                                            IdentifierName(classArgumentName))),
                                                    Token(SyntaxKind.CommaToken),
                                                    Argument
                                                    (
                                                        IdentifierName("cancellationToken"))
                                                }))))))));
    }
}