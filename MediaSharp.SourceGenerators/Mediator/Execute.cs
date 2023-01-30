using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace MediaSharp.SourceGenerators.Mediator
{
    internal static class Execute
    {
        public static bool TryGetClassInfo(
            ClassDeclarationSyntax classSyntax,
            GeneratorAttributeSyntaxContext ctx,
            out IAssemblySymbol namespaceName)
        {
            var classSymbol = ModelExtensions.GetDeclaredSymbol(ctx.SemanticModel, classSyntax) as ITypeSymbol;

            var isClassMediaRegistrable =
                classSymbol.AllInterfaces.Any(x => x.Name is "IRequestHandler");

            namespaceName = classSymbol.ContainingAssembly;

            return isClassMediaRegistrable;
        }

        public static CompilationUnitSyntax GetKnownClassSyntax(ImmutableArray<(ClassDeclarationSyntax, IAssemblySymbol)> item)
        {
            var memberDeclSyntax = new SyntaxList<MemberDeclarationSyntax>();
            
            foreach (var classSymbol in item)
            {
                var constructorInfo = classSymbol.Item1.Members.FirstOrDefault(x => x is ConstructorDeclarationSyntax);

                var parametersInfo = (constructorInfo as ConstructorDeclarationSyntax)?.ParameterList;

                var includedContextParametersSyntax = parametersInfo?.AddParameters(CreateMediaSharpContextInjection());

                var ctorSyntax = CreateConstuctorBased(classSymbol.Item1, includedContextParametersSyntax);

                if (parametersInfo is not null)
                {
                    ctorSyntax = ctorSyntax.WithInitializer(
                        ConstructorInitializer(
                            SyntaxKind.ThisConstructorInitializer,
                            CreateConstructorInitArgSyntax(parametersInfo)
                        ));
                }

                var currentGeneratedCtor = new SyntaxList<MemberDeclarationSyntax>(ctorSyntax);

                var decl = 
                    NamespaceDeclaration(
                        IdentifierName(classSymbol.Item2.Identity.Name)).WithMembers(
                    SingletonList<MemberDeclarationSyntax>(
                    ClassDeclaration(classSymbol.Item1.Identifier)
                        .WithModifiers(
                            TokenList(
                                new[]
                                {
                                    Token(SyntaxKind.PublicKeyword),
                                    Token(SyntaxKind.PartialKeyword)
                                }))
                .WithMembers(currentGeneratedCtor)));

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
    }
}
