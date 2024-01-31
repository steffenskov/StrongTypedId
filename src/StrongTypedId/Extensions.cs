using System;
using StrongTypedId;

public static class Extensions
{
	public static bool IsStrongTypedValue<T>(this T item)
	{
		if (item is not null)
			return item is IStrongTypedValue;

		var type = typeof(T);
		if (type == typeof(object)) // Type information gives us nothing, in this case we throw
			ArgumentNullException.ThrowIfNull(item);
		
		return typeof(T).GetInterface("IStrongTypedValue") is not null;
	}
}