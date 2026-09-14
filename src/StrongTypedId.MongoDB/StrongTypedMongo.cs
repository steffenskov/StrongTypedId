using System.Reflection;
using StrongTypedId.MongoDB.Serializers.Factories;

namespace StrongTypedId.MongoDB;

public static class StrongTypedMongo
{
	private static readonly object _serializerRegistrationLock = new();
	private static readonly HashSet<Type> _registeredSerializerTypes = [];

	public static void AddStrongTypedMongoSerializers(params Assembly[] assemblies)
	{
		var types = assemblies.SelectMany(assembly => assembly.GetTypes());
		var serializerFactories = CreateFactories().ToList();


		Parallel.ForEach(types, type =>
		{
			lock (_serializerRegistrationLock)
			{
				if (type.IsAbstract || _registeredSerializerTypes.Contains(type))
				{
					return;
				}
			}

			var factory = serializerFactories.Find(serializer => serializer.CanSerialize(type));
			if (factory is null)
			{
				return;
			}

			lock (_serializerRegistrationLock)
			{
				if (_registeredSerializerTypes.Contains(type))
				{
					return;
				}

				BsonSerializer.RegisterSerializer(type, factory.CreateSerializer(type));
				_registeredSerializerTypes.Add(type);
			}
		});
	}

	private static IEnumerable<IMongoSerializerFactory> CreateFactories()
	{
		var assemblyTypes = Assembly
			.GetAssembly(typeof(IMongoSerializerFactory))!
			.GetTypes()
			.Where(type => type is { IsAbstract: false, IsInterface: false });

		foreach (var type in assemblyTypes)
		{
			if (type.IsAssignableTo(typeof(IMongoSerializerFactory)))
			{
				yield return (IMongoSerializerFactory)Activator.CreateInstance(type)!;
			}
		}
	}
}