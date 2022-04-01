using Microsoft.CodeAnalysis;


namespace Weknow.EventSource.Backbone
{
    [Generator]
    internal class ContractGenerator : ISourceGenerator
    {
        private const string TARGET_ATTRIBUTE = "Flags";

        public ContractGenerator()
        {

        }

        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForSyntaxNotifications(() => new SyntaxReceiver(TARGET_ATTRIBUTE));
        }


        public void Execute(GeneratorExecutionContext context)
        {
            if (context.SyntaxReceiver is not SyntaxReceiver syntax || syntax.TargetAttribute != TARGET_ATTRIBUTE) return;

            context.AddSource($"test.gen.cs", $"Test {DateTime.Now: HH:mm:ss}");
        }

    }
}
