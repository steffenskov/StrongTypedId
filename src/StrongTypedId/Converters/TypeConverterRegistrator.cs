using System.ComponentModel;
using System.Diagnostics;
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
		var strongTypedValueType = GetStrongTypedValueType(type);
		var arguments = strongTypedValueType.GetGenericArguments();
		var tself = arguments[0];
		var tprimitive = arguments[1];
		var converterType = typeof(StrongTypedValueTypeConverter<,>).MakeGenericType(tself, tprimitive);
		TypeDescriptor.AddAttributes(tself, new TypeConverterAttribute(converterType));
	}

	private static Type GetStrongTypedValueType(Type type)
	{
		if (type is { IsAbstract: true, IsGenericType: true } && type.GetGenericTypeDefinition() == typeof(StrongTypedValue<,>))
		{
			return type;
		}

		if (type.BaseType is not null)
		{
			return GetStrongTypedValueType(type.BaseType);
		}

		throw new UnreachableException($"Type {type.Name} does not inherit StrongTypedValue<,> but DOES implement IStrongTypedValue, this should not happen.");
	}
}