// credit: https://github.com/juarezasjunior/WoodyFramework/blob/da41b058b6ef5615329f8715a018f57018968eba/src/WoodyFramework/WoodyFramework.CodeGenerators/MainIncrementalGenerator.cs

namespace Bnaya.Samples
{
    using System.Collections.Immutable;
    using System.IO;
    using System.Linq;

    using Microsoft.CodeAnalysis;

    //using WoodyFramework.CodeGenerators.Helpers;
    //using WoodyFramework.CodeGenerators.Structure;



    [Generator]
    internal class TextGen : IIncrementalGenerator
    {
        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            /*#if DEBUG
                        if (!Debugger.IsAttached)
                        {
                            Debugger.Launch();
                        }
            #endif*/

            IncrementalValuesProvider<AdditionalText> providerSettingsAdditionalTextList = context.AdditionalTextsProvider.Where(static file => file.Path.EndsWith(".txt"));
            IncrementalValuesProvider<(string TxtFileName, string TxtContent)> providerTxtFiles = providerSettingsAdditionalTextList.Select(
                (text, cancellationToken) =>
                   (
                       FileName: Path.GetFileName(text.Path),
                       Content: text.GetText(cancellationToken)?.ToString())
                   );

            IncrementalValueProvider<ImmutableArray<(string TxtFileName, string TxtContent)>> collected = providerTxtFiles.Collect();

            context.RegisterSourceOutput(collected, Generate);
        }

        private static void Generate(
            SourceProductionContext spc,
            ImmutableArray<(string TxtFileName, string TxtContent)> items)
        {

            foreach (var nameAndContent in items)
            {
                var (TxtFileName, TxtContent) = nameAndContent;
                spc.AddSource($"TextGen.{TxtFileName}.cs", $@"
    public static partial class ConstStrings
    {{
        public const string {TxtFileName} = ""{TxtContent}"";
    }}");
            }
        }
    }
}