//using System;
//using System.Collections.Immutable;
//using System.Text;

//using Microsoft.CodeAnalysis;
//using Microsoft.CodeAnalysis.CSharp.Syntax;
//using Microsoft.CodeAnalysis.Text;

//// Incremental Generators: https://github.com/dotnet/roslyn/blob/main/docs/features/incremental-generators.md
//// Doc: https://bnaya.visualstudio.com/Weknow/_wiki/wikis/Weknow.wiki/604/Source-Code-Generator-(.NET)
//// Source Generator V2: https://andrewlock.net/exploring-dotnet-6-part-9-source-generator-updates-incremental-generators/

//namespace Bnaya.Samples
//{
//    [Generator]
//    public class SampleGenerator : IIncrementalGenerator
//    {
//        public void Initialize(IncrementalGeneratorInitializationContext context)
//        {
//            IncrementalValuesProvider<ClassDeclarationSyntax?> classDeclarations =
//                context.SyntaxProvider
//                .CreateSyntaxProvider(
//                    predicate: static (s, _) => IsSyntaxTargetForGeneration(s),
//                    transform: static (ctx, _) => GetSemanticTargetForGeneration(ctx))
//                .Where(static m => m is not null);



//            IncrementalValueProvider<(Compilation, ImmutableArray<ClassDeclarationSyntax?>)> compilationAndClasses
//                = context.CompilationProvider.Combine(classDeclarations.Collect());

//            context.RegisterSourceOutput(compilationAndClasses,
//                static (spc, source) => Execute(source.Item1, source.Item2, spc));

//        }


//        private static bool IsSyntaxTargetForGeneration(SyntaxNode node)
//            => node is MethodDeclarationSyntax m && m.AttributeLists.Count > 0;

//        private static ClassDeclarationSyntax? GetSemanticTargetForGeneration(GeneratorSyntaxContext context)
//        {
//            // we know the node is a MethodDeclarationSyntax thanks to IsSyntaxTargetForGeneration
//            var methodDeclarationSyntax = (MethodDeclarationSyntax)context.Node;

//            // loop through all the attributes on the method
//            foreach (AttributeListSyntax attributeListSyntax in methodDeclarationSyntax.AttributeLists)
//            {
//                foreach (AttributeSyntax attributeSyntax in attributeListSyntax.Attributes)
//                {
//                    IMethodSymbol attributeSymbol = context.SemanticModel.GetSymbolInfo(attributeSyntax).Symbol as IMethodSymbol;
//                    if (attributeSymbol == null)
//                    {
//                        // weird, we couldn't get the symbol, ignore it
//                        continue;
//                    }

//                    INamedTypeSymbol attributeContainingTypeSymbol = attributeSymbol.ContainingType;
//                    string fullName = attributeContainingTypeSymbol.ToDisplayString();

//                    // Is the attribute the [LoggerMessage] attribute?
//                    if (fullName == "EchoMeAttribute")
//                    {
//                        // return the parent class of the method
//                        return methodDeclarationSyntax.Parent as ClassDeclarationSyntax;
//                    }
//                }
//            }

//            // we didn't find the attribute we were looking for
//            return null;
//        }

//        private static void Execute(Compilation compilation, ImmutableArray<ClassDeclarationSyntax?> classes, SourceProductionContext context)
//        {
//            if (classes.IsDefaultOrEmpty)
//            {
//                // nothing to do yet
//                return;
//            }

//            IEnumerable<ClassDeclarationSyntax> distinctClasses = classes.Distinct();

//            var p = new Parser(compilation, context.ReportDiagnostic, context.CancellationToken);

//            IReadOnlyList<LoggerClass> logClasses = p.GetLogClasses(distinctClasses);
//            if (logClasses.Count > 0)
//            {
//                var e = new Emitter();
//                string result = e.Emit(logClasses, context.CancellationToken);

//                context.AddSource("LoggerMessage.g.cs", SourceText.From(result, Encoding.UTF8));
//            }
//        }
//    }
//}
