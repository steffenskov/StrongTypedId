namespace StrongTypedId.MongoDB.Serializers.Factories;

internal class StrongTypedStringSerializerFactory : IMongoSerializerFactory
{
	public bool CanSerialize(Type type)
	{
		if (type.BaseType is null)
		{
			return false;
		}

		var baseType = type.BaseType;
		while (baseType is not null)
		{
			var isBaseType = baseType.IsGenericType && baseType.GetGenericTypeDefinition() == typeof(StrongTypedValue<,>);
			if (isBaseType)
			{
				break;
			}

			baseType = baseType.BaseType;
		}

		if (baseType is null)
		{
			return false;
		}

		var genericArguments = baseType.GetGenericArguments();
		return genericArguments[0] == type && genericArguments[1] == typeof(string);
	}

	public IBsonSerializer CreateSerializer(Type type)
	{
		var strongTypeSerializerType = typeof(StrongTypedStringSerializer<>).MakeGenericType(type);
		return (IBsonSerializer)Activator.CreateInstance(strongTypeSerializerType)!;
	}
}