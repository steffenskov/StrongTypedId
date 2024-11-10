namespace StrongTypedId.MongoDB.Serializers.Factories;

internal class StrongTypedGuidSerializerFactory : IMongoSerializerFactory
{
	public bool CanSerialize(Type type)
	{
		if (type.BaseType is null)
		{
			return false;
		}

		if (!type.BaseType.IsGenericType)
		{
			return false;
		}

		return type.BaseType.GetGenericTypeDefinition() == typeof(StrongTypedGuid<>);
	}

	public IBsonSerializer CreateSerializer(Type type)
	{
		var strongTypeSerializerType = typeof(StrongTypedGuidSerializer<>).MakeGenericType(type);
		return (IBsonSerializer)Activator.CreateInstance(strongTypeSerializerType)!;
	}
}