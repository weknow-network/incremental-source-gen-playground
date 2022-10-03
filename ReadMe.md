# Playground for incremental source generators

- [Official Docs: Incremental Generators](https://github.com/dotnet/roslyn/blob/main/docs/features/incremental-generators.md)
- [.NET 6 inprovement](https://andrewlock.net/exploring-dotnet-6-part-9-source-generator-updates-incremental-generators/)
- [API Client with C# Source Generators](https://sharovarskyi.com/blog/posts/source-generators-api-client/)
- [Source Generators Cookbook](https://github.com/dotnet/roslyn/blob/main/docs/features/source-generators.cookbook.md)
- [Introducing Source Generators](https://devblogs.microsoft.com/dotnet/introducing-c-source-generators/?WT.mc_id=dotnet-00000-cephilli)
- [FAQ (video)](https://www.youtube.com/watch?v=pqLs7X6Cr6s)
- [Video](https://docs.microsoft.com/en-us/shows/On-NET/C-Source-Generators)


## Attach to project reference

Add: `ReferenceOutputAssembly="false" OutputItemType="Analyzer"` to the ProjectReference entry

``` xml
<ItemGroup>
  <ProjectReference Include="xxx.csproj" ReferenceOutputAssembly="false" OutputItemType="Analyzer" />
</ItemGroup>
```

Properties of the Genetator code:
```xml
	<PropertyGroup>
		<GeneratePackageOnBuild>true</GeneratePackageOnBuild>
		<IncludeSource>True</IncludeSource>
		<IncludeSymbols>True</IncludeSymbols>
		<GenerateDocumentationFile>True</GenerateDocumentationFile>
		<EnableNETAnalyzers>True</EnableNETAnalyzers>
		<!--<AnalysisLevel>6.0-all</AnalysisLevel>-->
	</PropertyGroup>
```

```xml
	<PropertyGroup>
		<TargetFramework>netstandard2.0</TargetFramework>
		<LangVersion>10</LangVersion>
		<Nullable>enable</Nullable>
		<IsRoslynComponent>true</IsRoslynComponent>
		<EmitCompilerGeneratedFiles>true</EmitCompilerGeneratedFiles>
		<CompilerGeneratedFilesOutputPath>Generated</CompilerGeneratedFilesOutputPath>
		<IsRoslynComponent>true</IsRoslynComponent>
		<IncludeBuildOutput>false</IncludeBuildOutput>
	</PropertyGroup>
```

[Project configuration (for debug)](https://github.com/JoanComasFdz/dotnet-how-to-debug-source-generator-vs2022)

## Debug
- [Debug Source Generator](http://stevetalkscode.co.uk/debug-source-generators-with-vs2019-1610)
- [Source Generator Playground - code](https://github.com/davidwengier/SourceGeneratorPlayground)
  - [online](https://wengier.com/SourceGeneratorPlayground/)

## Templates
- [Basic Source Generator template V1](https://github.com/davidwengier/SourceGeneratorTemplate)

## Practical
- [NSwagStudio](/Weknow-Knowledge/How-To-\(Weknow\)/Manuals/Automations/Source-Code-Generator-\(.NET\)/NSwagStudio)
- [Mapperly (object mapping)](https://github.com/riok/mapperly)
- [DevTeam/Pure.DI](https://github.com/DevTeam/Pure.DI#aspnet-core-blazor)
- [Data Builder Generator (Fluent API)](https://github.com/dasMulli/data-builder-generator)
- [SimpleSIMD](https://github.com/giladfrid009/SimpleSIMD)
- [SpreadCheetah (Excel Generator - forward only)](https://github.com/sveinungf/spreadcheetah)
- [GraphQL.Tools (from gql -> .NET classes)](https://github.com/MoienTajik/GraphQL.Tools)
- [Generator.Equals](https://github.com/diegofrata/Generator.Equals)
- [ScenarioTests](https://github.com/koenbeuk/ScenarioTests)
- [HttpClientCodeGenerator](https://github.com/Jalalx/HttpClientCodeGenerator)

## Sample
- [Refit: The automatic type-safe REST library](https://github.com/reactiveui/refit?s=08)
- [Source Generator Samples (GitHub)](https://github.com/dotnet/roslyn-sdk/tree/main/samples/CSharp/SourceGenerators)
- [Doc & Sample collection](https://github.com/amis92/csharp-source-generators)
- [101 public repositories](https://github.com/topics/csharp-sourcegenerator)
- [CSV C# Source Generator samples](https://devblogs.microsoft.com/dotnet/new-c-source-generator-samples/?WT.mc_id=dotnet-00000-cephilli)
- [Mapping at build time](https://cezarypiatek.github`.io/post/generate-mappings-on-build/)

## More
- [Intro - general concept (video)](https://channel9.msdn.com/Shows/On-NET/C-Source-Generators)
- [deeper tutorial (video)](https://channel9.msdn.com/Events/dotnetConf/2020/C-Source-Generators-Write-Code-that-Writes-Code)


- Analyzer (which could be migrate to Code Gen):
  - [MappingGenerator](https://github.com/cezarypiatek/MappingGenerator)