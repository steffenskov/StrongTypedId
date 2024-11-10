using Ckode;
using StrongTypedId.MongoDB.Serializers.Factories;

namespace StrongTypedId.MongoDB;

public static class StrongTypedMongo
{
	private static readonly object _serializerRegistrationLock = new();
	private static readonly HashSet<Type> _registeredSerializerTypes = [];

	public static void AddStrongTypedMongoSerializers(params Type[] typeHooks)
	{
		var types = typeHooks.SelectMany(type => type.Assembly.GetTypes());
		var serializerFactories = ServiceLocator.CreateInstances<IMongoSerializerFactory>().ToList();


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
}