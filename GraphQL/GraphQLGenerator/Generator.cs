using System.Collections.Immutable;
using System.Text;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

public class GenResult
{
    public GenResult(ClassDeclarationSyntax syntax, ISymbol symbol)
    {
        Syntax = syntax;
        Symbol = symbol;
    }

    public ClassDeclarationSyntax Syntax { get; }
    public ISymbol Symbol { get; }
}

[Generator]
public class Generator : IIncrementalGenerator
{
    private const string TARGET_ATTRIBUTE = "MarkerAttibute";

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
#pragma warning disable CS8619
        IncrementalValuesProvider<GenResult> classDeclarations =
                context.SyntaxProvider
                    .CreateSyntaxProvider(
                        predicate: static (s, _) => IsSyntaxTargetForGeneration(s),
                        transform: static (ctx, _) => GetSemanticTargetForGeneration(ctx))
                    .Where(static m => m is not null);
#pragma warning restore CS8619

        static bool IsSyntaxTargetForGeneration(SyntaxNode node)
        {
            if(!(node is ClassDeclarationSyntax c)) return false;

            bool hasAttributes = c.AttributeLists.Any(m => m.Attributes.Any(m1 => m1.Name.ToString() == TARGET_ATTRIBUTE));

            return hasAttributes;
        };

        IncrementalValueProvider<(Compilation, ImmutableArray<GenResult>)> compilationAndClasses
            = context.CompilationProvider.Combine(classDeclarations.Collect());

        context.RegisterSourceOutput(compilationAndClasses, Generate);
    }

    static void Generate(
        SourceProductionContext spc,
        (Compilation compilation, 
        ImmutableArray<GenResult> items) source)
    {
        var (compilation, items) = source;
        foreach (GenResult item in items)
        {
            Generate(spc, compilation, item);
        }
    }

    static void Generate(
        SourceProductionContext spc,
        Compilation compilation, 
        GenResult item)
    {
        var symbol = item.Symbol;
        var syntax = item.Syntax;
        AttributeData att = symbol.GetAttributes().Single(m => m.AttributeClass?.Name == TARGET_ATTRIBUTE);
        var map = att.NamedArguments.ToDictionary(m => m.Key, m => m.Value.Value?.ToString() ?? string.Empty);
        var operationName = map["OperationName"];
        var description = map["Description"];

        var cls = syntax.Identifier.Text;
        StringBuilder sb = new ();
        sb.AppendLine(@$"
class {cls}QlWrapper
{{
    // {description ?? $"GraphQL entity wrapper of {cls}"}
    public {cls} {operationName} {{ get; init; }}
}}
");
        spc.AddSource("gen1.cs", sb.ToString());
    }

    static GenResult GetSemanticTargetForGeneration(GeneratorSyntaxContext context)
    {
        var clsDeclarationSyntax = (ClassDeclarationSyntax)context.Node;
        var symbol = context.SemanticModel.GetDeclaredSymbol(clsDeclarationSyntax);
        if (symbol == null) throw new NullReferenceException($"Code generated symbol of {nameof(clsDeclarationSyntax)} is missing"); 
        return new GenResult(clsDeclarationSyntax, symbol);
    }
}