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
		if (type == typeof(object)) // Type information gives us nothing, in this case we throw
		{
			return false;
		}

		return type.GetInterface(nameof(IStrongTypedValue)) is not null;
	}

	public static bool IsStrongTypedGuid(this Type type)
	{
		if (type == typeof(object)) // Type information gives us nothing, in this case we throw
		{
			return false;
		}

		return type.GetInterface(nameof(IStrongTypedGuid)) is not null;
	}
}