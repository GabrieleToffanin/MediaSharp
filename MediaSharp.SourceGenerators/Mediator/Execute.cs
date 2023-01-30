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
            GeneratorAttributeSyntaxContext ctx)
        {
            var classSymbol = ModelExtensions.GetDeclaredSymbol(ctx.SemanticModel, classSyntax) as ITypeSymbol;
            var isClassMediaRegistrable =
                classSymbol.AllInterfaces.Any(x => x.Name is "IRequestHandler");

            return isClassMediaRegistrable;
        }

        public static CompilationUnitSyntax GetKnownClassSyntax(ImmutableArray<ClassDeclarationSyntax> item)
        {
            var memberDeclSyntax = new SyntaxList<MemberDeclarationSyntax>();
            foreach (var classSymbol in item)
            {
                var constructorInfo = classSymbol.Members.FirstOrDefault(x => x is ConstructorDeclarationSyntax);

                var parametersInfo = (constructorInfo as ConstructorDeclarationSyntax)?.ParameterList;

                var includedContextParametersSyntax = parametersInfo.AddParameters(
                    Parameter(
                            Identifier("context"))
                        .WithType(
                            IdentifierName("MediaSharp.Core.MediatorContext"))
                );

                var decl = 
                    ClassDeclaration(classSymbol.Identifier)
                        .WithModifiers(
                            TokenList(
                                new[]
                                {
                                    Token(SyntaxKind.PublicKeyword),
                                    Token(SyntaxKind.PartialKeyword)
                                }))
                .WithMembers(
                    SingletonList<MemberDeclarationSyntax>(
                        ConstructorDeclaration(
                                classSymbol.Identifier)
                            .WithModifiers(
                                TokenList(
                                    Token(SyntaxKind.PublicKeyword)))
                            .WithParameterList(includedContextParametersSyntax)
                            .WithInitializer(
                                ConstructorInitializer(
                                    SyntaxKind.ThisConstructorInitializer,
                                    CreateConstructorInitArgSyntax(parametersInfo)
                                    ))
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
                                                                ThisExpression()))))))))));

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
    }
}
