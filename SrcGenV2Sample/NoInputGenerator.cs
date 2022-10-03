using System;
using System.Collections.Immutable;
using System.Text;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;


namespace Bnaya.Samples
{
    [Generator]
    public class NoInputGenerator : IIncrementalGenerator
    {
        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            context.RegisterPostInitializationOutput(ctx =>
            {
                ctx.AddSource("GenTime", @$"
static class GenTime {{  
    public static string GetAt => ""{DateTime.Now.ToString()}"";
}}
");
            });
        }
    }
}