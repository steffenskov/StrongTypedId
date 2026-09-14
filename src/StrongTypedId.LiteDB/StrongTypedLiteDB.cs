using System.Reflection;
using StrongTypedId.LiteDB.Serializers.Factories;

namespace StrongTypedId.LiteDB;

public static class StrongTypedLiteDB
{
	public static BsonMapper CreateBsonMapper(params IEnumerable<Assembly> assemblies)
	{
		HashSet<Type> registeredSerializerTypes = [];
		object serializerRegistrationLock = new();
		var types = assemblies.SelectMany(assembly => assembly.GetTypes());

		var serializerFactories = CreateFactories().ToList();

		var mapper = new BsonMapper();

		foreach (var type in types)
		{
			if (type.IsAbstract || type.IsInterface || type.IsValueType || type.BaseType is null) // Cannot be StrongTyped inheritances
			{
				continue;
			}

			lock (serializerRegistrationLock)
			{
				if (registeredSerializerTypes.Contains(type))
				{
					continue;
				}

				var factory = serializerFactories.Find(serializer => serializer.CanSerialize(type));
				if (factory is null)
				{
					continue;
				}

				var serializer = factory.CreateSerializer(type);
				mapper.RegisterType(type, serializer.Serialize, serializer.Deserialize);
				registeredSerializerTypes.Add(type);
			}
		}

		return mapper;
	}

	private static IEnumerable<ILiteDBSerializerFactory> CreateFactories()
	{
		var assemblyTypes = Assembly
			.GetAssembly(typeof(ILiteDBSerializerFactory))!
			.GetTypes()
			.Where(type => type is { IsAbstract: false, IsInterface: false });

		foreach (var type in assemblyTypes)
		{
			if (type.IsAssignableTo(typeof(ILiteDBSerializerFactory)))
			{
				yield return (ILiteDBSerializerFactory)Activator.CreateInstance(type)!;
			}
		}
	}
}