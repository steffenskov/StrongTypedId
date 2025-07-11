using System.Runtime.CompilerServices;

namespace StrongTypedId.NewtonSoft.Validation;

static internal class JsonConverterCorrectnessScanner
{
#pragma warning disable CA2255
	[ModuleInitializer]
#pragma warning restore CA2255
	public static void Initialize()
	{
		var validator = new ConverterAttributeValidator();
		foreach (var type in GetStrongTypedIdTypes())
		{
			validator.ValidateType(type);
		}
	}

	private static IEnumerable<Type> GetStrongTypedIdTypes()
	{
		var types = AppDomain.CurrentDomain
			.GetAssemblies()
			.SelectMany(assembly =>
			{
				try
				{
					return assembly.GetTypes();
				}
				catch
				{
					return [];
				}
			});
		return types.Where(type => type is { IsClass: true, IsAbstract: false, IsGenericType: false } && type.IsStrongTypedValue());
	}
}