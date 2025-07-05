using System.Diagnostics.CodeAnalysis;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace StrongTypedId.Generators;

[Generator]
[SuppressMessage("MicrosoftCodeAnalysisCorrectness", "RS1035:Do not use APIs banned for analyzers")]
public class StrongTypedValueJsonConverterGenerator : IIncrementalGenerator
{
	public void Initialize(IncrementalGeneratorInitializationContext context)
	{
		// Create a provider for class declarations that might inherit from StrongTypedValue<TSelf, TPrimitiveValue>
		var classDeclarations = context.SyntaxProvider
			.CreateSyntaxProvider(
				static (s, _) => IsCandidateClass(s),
				static (ctx, _) => GetClassDeclaration(ctx))
			.Where(static m => m is not null);

		// Combine with compilation
		var compilationAndClasses = context.CompilationProvider.Combine(classDeclarations.Collect());

		// Generate source for each valid class
		context.RegisterSourceOutput(compilationAndClasses, static (spc, source) =>
		{
			var (compilation, classes) = source;
			Execute(compilation, classes.Where(c => c is not null), spc);
		});
	}

	private static bool IsCandidateClass(SyntaxNode node)
	{
		return node is ClassDeclarationSyntax { BaseList.Types.Count: > 0 };
	}

	private static ClassDeclarationSyntax? GetClassDeclaration(GeneratorSyntaxContext context)
	{
		var classDeclaration = (ClassDeclarationSyntax)context.Node;

		// Quick check if it might be a StrongTypedValue pattern
		if (classDeclaration.BaseList?.Types.Any() == true)
		{
			return classDeclaration;
		}

		return null;
	}

	private static void Execute(Compilation compilation, IEnumerable<ClassDeclarationSyntax?> classes, SourceProductionContext context)
	{
		var strongTypedValueSymbol = compilation.GetTypeByMetadataName("StrongTypedId.StrongTypedValue`2");

		if (strongTypedValueSymbol == null)
		{
			return;
		}

		foreach (var candidateClass in classes)
		{
			if (candidateClass == null)
			{
				continue;
			}

			var semanticModel = compilation.GetSemanticModel(candidateClass.SyntaxTree);
			var classSymbol = semanticModel.GetDeclaredSymbol(candidateClass);

			if (classSymbol == null || classSymbol.IsAbstract)
			{
				continue;
			}

			// Check if this class inherits from StrongTypedValue<TSelf, TPrimitiveValue> pattern
			var strongTypedValueInfo = GetStrongTypedValueInfo(classSymbol, strongTypedValueSymbol);
			if (strongTypedValueInfo != null)
			{
				var source = GenerateJsonConverterAttribute(classSymbol, strongTypedValueInfo.Value);
				context.AddSource($"{classSymbol.Name}_JsonConverter.g.cs", SourceText.From(source, Encoding.UTF8));
			}
		}
	}

	private static (INamedTypeSymbol TSelf, ITypeSymbol TPrimitiveValue)? GetStrongTypedValueInfo(
		INamedTypeSymbol classSymbol,
		INamedTypeSymbol strongTypedValueSymbol)
	{
		var baseType = classSymbol.BaseType;

		while (baseType != null)
		{
			if (SymbolEqualityComparer.Default.Equals(baseType.OriginalDefinition, strongTypedValueSymbol))
			{
				// Check if it follows the StrongTypedValue<TSelf, TPrimitiveValue> pattern
				var typeArguments = baseType.TypeArguments;
				if (typeArguments.Length == 2)
				{
					var tSelf = typeArguments[0];
					var tPrimitiveValue = typeArguments[1];

					// Verify TSelf matches the current class (the StrongTypedValue<TSelf, TPrimitiveValue> pattern)
					if (SymbolEqualityComparer.Default.Equals(tSelf, classSymbol))
					{
						return (classSymbol, tPrimitiveValue);
					}
				}
			}

			baseType = baseType.BaseType;
		}

		return null;
	}

	private static string GenerateJsonConverterAttribute(INamedTypeSymbol classSymbol, (INamedTypeSymbol TSelf, ITypeSymbol TPrimitiveValue) info)
	{
		var namespaceName = classSymbol.ContainingNamespace.ToDisplayString();
		var className = classSymbol.Name;
		var tSelfName = info.TSelf.Name;
		var tPrimitiveValueName = info.TPrimitiveValue.ToDisplayString();

		var source = new StringBuilder();

		// Add necessary usings
		source.AppendLine("using StrongTypedId.Converters;");
		source.AppendLine();

		// Add namespace if exists
		if (!string.IsNullOrEmpty(namespaceName) && namespaceName != "<global namespace>")
		{
			source.AppendLine($"namespace {namespaceName}");
			source.AppendLine("{");
		}

		// Generate the partial class with JsonConverter attribute
		var indent = string.IsNullOrEmpty(namespaceName) || namespaceName == "<global namespace>" ? "" : "    ";

		source.AppendLine($"{indent}[StrongTypedValueJsonConverter<{tSelfName}, {tPrimitiveValueName}>]");
		source.AppendLine($"{indent}public partial class {className}");
		source.AppendLine($"{indent}{{");
		source.AppendLine($"{indent}}}");

		// Close namespace if exists
		if (!string.IsNullOrEmpty(namespaceName) && namespaceName != "<global namespace>")
		{
			source.AppendLine("}");
		}

		File.AppendAllText("/tmp/debug.txt", source.ToString());
		
		
		return source.ToString();
	}
}