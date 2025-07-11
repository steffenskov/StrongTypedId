using StrongTypedId;

public static class Extensions
{
	public static bool IsStrongTypedValue<T>(this T item)
	{
		if (item is not null)
		{
			return item is IStrongTypedValue;
		}

		var type = typeof(T);
		if (type == typeof(object)) // Type information gives us nothing, in this case we throw
		{
			ArgumentNullException.ThrowIfNull(item);
		}

		return type.GetInterface(nameof(IStrongTypedValue)) is not null;
	}

	public static bool IsStrongTypedValue(this Type type)
	{
		if (type == typeof(object))
		{
			return false;
		}

		return type.GetInterface(nameof(IStrongTypedValue)) is not null;
	}

	public static bool IsStrongTypedId(this Type type)
	{
		if (type == typeof(object))
		{
			return false;
		}

		return type.GetInterface(nameof(IStrongTypedId)) is not null;
	}

	public static bool IsStrongTypedGuid(this Type type)
	{
		if (type == typeof(object))
		{
			return false;
		}

		return type.GetInterface(nameof(IStrongTypedGuid)) is not null;
	}

	/// <summary>
	///     Gets the StrongTypedValue type specification for a given type, should not be called unless IsStrongTypedValue has
	///     been checked first.
	/// </summary>
	public static Type GetStrongTypedValueType(this Type type)
	{
		if (type is { IsAbstract: true, IsGenericType: true } && type.GetGenericTypeDefinition() == typeof(StrongTypedValue<,>))
		{
			return type;
		}

		if (type.BaseType is not null)
		{
			return GetStrongTypedValueType(type.BaseType);
		}

		throw new InvalidOperationException($"Type {type.Name} does not inherit StrongTypedValue<,>");
	}

	/// <summary>
	///     Gets the generic type arguments to a StrongTypedValue type, should not be called unless IsStrongTypedValue has
	///     been checked first.
	/// </summary>
	public static (Type TSelf, Type TPrimitive) GetStrongTypedValueArguments(this Type type)
	{
		var strongTypedValueType = type.GetStrongTypedValueType();
		var arguments = strongTypedValueType.GetGenericArguments();
		var tself = arguments[0];
		var tprimitive = arguments[1];

		return (tself, tprimitive);
	}
}