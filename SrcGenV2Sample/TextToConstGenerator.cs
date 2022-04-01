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
     //[Generator(LanguageNames.CSharp)]
    [Generator]
    public class TextToConstGenerator : IIncrementalGenerator
    {
        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            // define the execution pipeline here via a series of transformations:

            // find all additional files that end with .txt
            IncrementalValuesProvider<AdditionalText> textFiles = context.AdditionalTextsProvider
                                    .Where(FileFilter);

            // read their contents and save their name
            IncrementalValuesProvider<(string name, string content)> namesAndContents = 
                textFiles.Select(Transform);

            // generate a class that contains their values as const strings
            context.RegisterSourceOutput(namesAndContents, Generate);
        }

        private static bool FileFilter(AdditionalText file)
        {
            var res = file?.Path?.EndsWith(".txt", StringComparison.OrdinalIgnoreCase);
            return res ?? false;
        }

        private static (string name, string content) Transform(AdditionalText text, CancellationToken cancellationToken)
        {
            string name = Path.GetFileNameWithoutExtension(text.Path);
            string content = text.GetText(cancellationToken)!.ToString();
            return (name: name, content: content);
        }

        private static void Generate(
            SourceProductionContext spc,
            (string name, string content) nameAndContent)
        {
            spc.AddSource($"ConstStrings.{nameAndContent.name}", $@"
    public static partial class ConstStrings
    {{
        public const string {nameAndContent.name} = ""{nameAndContent.content}"";
    }}");
        }
    }
}
