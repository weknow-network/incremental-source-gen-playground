using System;
using System.Collections.Immutable;
using System.Text;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

// Incremental Generators: https://github.com/dotnet/roslyn/blob/main/docs/features/incremental-generators.md
// Doc: https://bnaya.visualstudio.com/Weknow/_wiki/wikis/Weknow.wiki/604/Source-Code-Generator-(.NET)
// Source Generator V2: https://andrewlock.net/exploring-dotnet-6-part-9-source-generator-updates-incremental-generators/

namespace Bnaya.Samples
{
    // https://github.com/dotnet/roslyn/blob/main/docs/features/incremental-generators.md#simple-example
     [Generator(LanguageNames.CSharp)]
    //[Generator]
    public class TextToConstGenerator : IIncrementalGenerator
    {
        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            // define the execution pipeline here via a series of transformations:

            // find all additional files that end with .txt
            IncrementalValuesProvider<AdditionalText> textFiles =
                context.AdditionalTextsProvider
                       .Where(static file => file.Path.EndsWith(".txt"));

            // read their contents and save their name
            IncrementalValuesProvider<(string name, string content)> namesAndContents = 
                textFiles.Select(
                    (text, cancellationToken) =>
                        (name: Path.GetFileNameWithoutExtension(text.Path), 
                         content: text.GetText(cancellationToken)!.ToString()));

            

            // generate a class that contains their values as const strings
            context.RegisterSourceOutput(namesAndContents, (spc, nameAndContent) =>
            {
                string fileName = $"ConstStrings.{nameAndContent.name}.gen.cs";
                string code = $@"
    public static partial class ConstStrings
    {{
        public const string {nameAndContent.name} = ""{nameAndContent.content}"";
    }}";
                spc.AddSource(
                    fileName,
                    code);
            });

        }

    }
}
