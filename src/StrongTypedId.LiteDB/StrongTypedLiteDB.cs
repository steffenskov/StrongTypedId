using System.Reflection;
using Ckode;
using StrongTypedId.LiteDB.Serializers.Factories;

namespace StrongTypedId.LiteDB;

public static class StrongTypedLiteDB
{
	public static BsonMapper CreateBsonMapper(params IEnumerable<Assembly> assemblies)
	{
		HashSet<Type> registeredSerializerTypes = [];
		object serializerRegistrationLock = new();
		var types = assemblies.SelectMany(assembly => assembly.GetTypes());
		var serializerFactories = ServiceLocator.CreateInstances<ILiteDBSerializerFactory>().ToList();

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
}