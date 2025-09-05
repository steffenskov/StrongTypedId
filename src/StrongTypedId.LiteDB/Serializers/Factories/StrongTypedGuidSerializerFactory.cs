namespace StrongTypedId.LiteDB.Serializers.Factories;

internal class StrongTypedGuidSerializerFactory : ILiteDBSerializerFactory
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

	public ILiteDBSerializer CreateSerializer(Type type)
	{
		var strongTypeSerializerType = typeof(StrongTypedGuidSerializer<>).MakeGenericType(type);
		return (ILiteDBSerializer)Activator.CreateInstance(strongTypeSerializerType)!;
	}
}