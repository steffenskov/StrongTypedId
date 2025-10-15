namespace StrongTypedId.LiteDB.Serializers.Factories;

internal class StrongTypedLongSerializerFactory : ILiteDBSerializerFactory
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

		var baseGenericType = type.BaseType.GetGenericTypeDefinition();
		var isBaseType = baseGenericType == typeof(StrongTypedValue<,>) || baseGenericType == typeof(StrongTypedId<,>);
		if (!isBaseType)
		{
			return false;
		}

		var genericArguments = type.BaseType.GetGenericArguments();
		return genericArguments[0] == type && genericArguments[1] == typeof(long);
	}

	public ILiteDBSerializer CreateSerializer(Type type)
	{
		var strongTypeSerializerType = typeof(StrongTypedLongSerializer<>).MakeGenericType(type);
		return (ILiteDBSerializer)Activator.CreateInstance(strongTypeSerializerType)!;
	}
}