using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Immutable;
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
            var fullArgumentNamespace = currentTypeTarget.ContainingNamespace.Name;
            var fullArgumentName = currentTypeTarget.Name;

            var separatorToken = string.IsNullOrEmpty(fullArgumentNamespace) ? "" : ".";

            argumentName = $"{fullArgumentNamespace}{separatorToken}{fullArgumentName}";
        }

        assemblySymbol = classSymbol.ContainingAssembly;
        containingNa = classSymbol.ContainingNamespace;

        return isClassMediaRegistrable;
    }

    public static CompilationUnitSyntax GetKnownClassSyntax(ImmutableArray<HandlerInfoSyntax> item)
    {
        var memberDeclSyntax = new SyntaxList<MemberDeclarationSyntax>();

        foreach (var classSymbol in item)
        {
            var constructorInfo = classSymbol.ClassDelc.Members.FirstOrDefault(x => x is ConstructorDeclarationSyntax);

            var parametersInfo = (constructorInfo as ConstructorDeclarationSyntax)?.ParameterList;

            var includedContextParametersSyntax = parametersInfo?.AddParameters(CreateMediaSharpContextInjection());

            var ctorSyntax = CreateConstuctorBased(classSymbol.ClassDelc, includedContextParametersSyntax);

            if (parametersInfo is not null)
            {
                ctorSyntax = ctorSyntax.WithInitializer(
                    ConstructorInitializer(
                        SyntaxKind.ThisConstructorInitializer,
                        CreateConstructorInitArgSyntax(parametersInfo)
                    ));
            }

            var decl =
                NamespaceDeclaration(
                    IdentifierName(classSymbol.Namespace.OriginalDefinition.ToString())).WithMembers(
                    SingletonList<MemberDeclarationSyntax>(
                        ClassDeclaration(classSymbol.ClassDelc.Identifier)
                            .WithModifiers(
                                TokenList(
                                    new[]
                                    {
                                        Token(SyntaxKind.PublicKeyword),
                                        Token(SyntaxKind.PartialKeyword)
                                    }))
                            .AddMembers(ctorSyntax, CreateMethodImplSyntax(classSymbol.RequestFull))));

            memberDeclSyntax = memberDeclSyntax.Add(decl);
        }

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


    private static ConstructorDeclarationSyntax CreateConstuctorBased(ClassDeclarationSyntax classSymbol, ParameterListSyntax includedContextParametersSyntax)
    {
        return ConstructorDeclaration(
                classSymbol.Identifier)
            .WithModifiers(
                TokenList(
                    Token(SyntaxKind.PublicKeyword)))
            .WithParameterList(includedContextParametersSyntax ??
                               ParameterList(
                                   SeparatedList<ParameterSyntax>(
                                       new SyntaxNodeOrToken[] { CreateMediaSharpContextInjection() })))
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
                                                ThisExpression()))))))));
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