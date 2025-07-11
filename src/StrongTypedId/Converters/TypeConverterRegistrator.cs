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
		foreach (var type in GetStrongTypedIdTypes())
		{
			RegisterTypeConverter(type);
		}
	}

	private static void RegisterTypeConverter(Type type)
	{
		var (tSelf, tPrimitive) = type.GetStrongTypedValueArguments();
		var converterType = typeof(StrongTypedValueTypeConverter<,>).MakeGenericType(tSelf, tPrimitive);
		TypeDescriptor.AddAttributes(tSelf, new TypeConverterAttribute(converterType));
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