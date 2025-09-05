using System.Reflection;
using Ckode;
using StrongTypedId.LiteDB.Serializers.Factories;

namespace StrongTypedId.LiteDB;

public static class StrongTypedLiteDB
{
	private static readonly object _serializerRegistrationLock = new();
	private static readonly HashSet<Type> _registeredSerializerTypes = [];

	public static BsonMapper CreateBsonMapper(params Assembly[] assemblies)
	{
		var types = assemblies.SelectMany(assembly => assembly.GetTypes());
		var serializerFactories = ServiceLocator.CreateInstances<ILiteDBSerializerFactory>().ToList();

		var mapper = new BsonMapper();

		foreach (var type in types)
		{
			lock (_serializerRegistrationLock)
			{
				if (type.IsAbstract || _registeredSerializerTypes.Contains(type))
				{
					continue;
				}
			}

			var factory = serializerFactories.Find(serializer => serializer.CanSerialize(type));
			if (factory is null)
			{
				continue;
			}

			lock (_serializerRegistrationLock)
			{
				if (_registeredSerializerTypes.Contains(type))
				{
					continue;
				}

				var serializer = factory.CreateSerializer(type);
				mapper.RegisterType(type, serializer.Serialize, serializer.Deserialize);
				_registeredSerializerTypes.Add(type);
			}
		}

		return mapper;
	}
}