using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace StrongTypedId.Converters;

static internal class TypeConverterRegistrator
{
#pragma warning disable CA2255
	[ModuleInitializer]
#pragma warning restore CA2255
	public static void Initialize()
	{
		var assemblies = AppDomain.CurrentDomain.GetAssemblies();
		foreach (var assembly in assemblies)
		{
			try
			{
				var types = assembly.GetTypes()
					.Where(type => type is { IsClass: true, IsAbstract: false, IsGenericType: false } && type.IsStrongTypedValue());
				foreach (var type in types)
				{
					RegisterTypeConverter(type);
				}
			}
			catch
			{
				// Empty by design, skip assemblies we cannot read (usually obfuscated 3rd party libraries
			}
		}
	}

	private static void RegisterTypeConverter(Type type)
	{
		var (tSelf, tPrimitive) = type.GetStrongTypedValueArguments();
		var converterType = typeof(StrongTypedValueTypeConverter<,>).MakeGenericType(tSelf, tPrimitive);
		TypeDescriptor.AddAttributes(tSelf, new TypeConverterAttribute(converterType));
	}
}