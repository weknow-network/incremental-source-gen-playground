//using System;
//using System.Collections.Immutable;
//using System.Text;

//using Microsoft.CodeAnalysis;
//using Microsoft.CodeAnalysis.CSharp.Syntax;
//using Microsoft.CodeAnalysis.Text;



//namespace Bnaya.Samples
//{
//    //[Generator(LanguageNames.CSharp)]
//    [Generator]
//    public  class FromTextGenerator: IIncrementalGenerator
//    {
//        public void Initialize(IncrementalGeneratorInitializationContext context)
//        {
//            var assemblyName = context.CompilationProvider.Select(static (c, _) => c.AssemblyName);
//            var texts = context.AdditionalTextsProvider;

//            var combined = texts.Combine(assemblyName);

//            context.RegisterSourceOutput(combined, (spc, pair) =>
//            {
//                var assemblyName = pair.Right;
//                spc.AddSource("test", "// test");
//                // produce source ...
//            });
//        }
//    }
//}