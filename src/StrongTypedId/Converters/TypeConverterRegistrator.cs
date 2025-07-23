using System.ComponentModel;
using System.Reflection;
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
		var types = LoadAssemblies()
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

	private static IEnumerable<Assembly> LoadAssemblies()
	{
		var dir = AppDomain.CurrentDomain.BaseDirectory;
		var assemblies = AppDomain.CurrentDomain.GetAssemblies();
		foreach (var assembly in assemblies)
		{
			yield return assembly;
		}

		var assemblyFileNames = assemblies.Select(x => x.Location).ToHashSet();
		foreach (var filename in new DirectoryInfo(dir).GetFiles("*.dll").Select(file => file.FullName))
		{
			if (!assemblyFileNames.Contains(filename))
			{
				Assembly assembly;
				try
				{
					assembly = Assembly.LoadFrom(filename);
				}
				catch
				{
					// Empty by design, obfuscated assemblies will trigger this
					continue;
				}

				yield return assembly;
			}
		}
	}
}